using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Listings;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Infrastructure.Repositories;
using AuctionManagementSystem.Persistence.Context;
using AutoMapper;
using Microsoft.AspNetCore.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    class OrderRepository : IOrderRepository
    {
        public readonly AuctionManagementDbContext _context;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IAssetsRepository _assetsRepository;
        private readonly string _baseUrl = "https://localhost:62627/";
        private readonly IMapper _mapper;

        public OrderRepository(AuctionManagementDbContext context, ITransactionRepository transactionRepository, IMapper mapper, ICartRepository cartRepository,IAssetsRepository assetsRepository)
        {
            _context
                = context;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _assetsRepository = assetsRepository;
        }

        public async Task<List<DirectSaleAssetDto>> GetAllOrders(int userId)
        {
            var orderItems = await _context.TblOrders
                .Where(o => o.UserId == userId)
                .Include(o => o.TblOrderAssets)
                    .ThenInclude(oa => oa.Asset)
                        .ThenInclude(a => a.TblAssetGalleries)
                .Include(o => o.TblOrderAssets)
                    .ThenInclude(oa => oa.Asset)
                        .ThenInclude(a => a.Category)
                .ToListAsync();

            var assetDtos = orderItems
                .SelectMany(order => order.TblOrderAssets)
                .Where(oa => oa.Asset != null)
                .Select(oa =>
                {
                    var galleries = oa.Asset.TblAssetGalleries ?? new List<TblAssetGallery>();

                    // Prefer SortOrder = 1 thumbnail, else fallback to first
                    var thumbnailPath = galleries
                        .Select(g => g.FilePath)
                        .FirstOrDefault();

                    return new DirectSaleAssetDto
                    {
                        AssetId = oa.Asset.AssetId,
                        Title = oa.Asset.Title,
                        CategoryId = oa.Asset.CategoryId,
                        Deposit = oa.Asset.Deposit,
                        MinIncrement = oa.Asset.MinIncrement,
                        Description = oa.Asset.Description,
                        IsDeleted = oa.Asset.IsDeleted,
                        SalesNotes = oa.Asset.SalesNotes,
                        Price = oa.Asset.StartingPrice,
                        IsAvailableForDirectSale = oa.Asset.IsAvailableForDirectSale,
                        CategoryName = oa.Asset.Category?.CategoryName ?? string.Empty,
                        ThumbnailUrl = string.IsNullOrEmpty(thumbnailPath) ? string.Empty : $"{_baseUrl}{thumbnailPath}"
                    };
                })
                .ToList();

            return assetDtos;
        }

        public async Task<TblOrder> AddOrder(int userId, List<int> assetIds)
        {
            var order = new TblOrder
            {
                UserId = userId,
                OrderStatus = "Pending",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = userId.ToString(),
                TblOrderAssets = assetIds.Select(id => new TblOrderAsset
                {
                    AssetId = id
                }).ToList()
            };

            _context.TblOrders.Add(order);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? order : null;
        }

        public async Task<List<DirectSaleAssetDto>> ConfirmPaymentAndCreateOrderAsync(int userId, List<int> assetIds)
        {
            var assets = new List<TblAsset>();

            for (int i = 0; i < assetIds.Count; i++)
            {
                var asset = await _context.TblAssets
                    .FirstOrDefaultAsync(a => a.AssetId == assetIds[i]);

                if (asset != null)
                {
                    assets.Add(asset);
                }
            }

            if (assets == null || !assets.Any())
                throw new InvalidOperationException("Please Add some Asset in the Orders");

            decimal totalAmount = assets.Sum(a => a.StartingPrice);

            // Step 2: Create Transaction
            var transaction = new TblTransaction
            {
                UserId = userId,
                Amount = totalAmount,
                TransactionTypeId = 1, // Default DirectSale type
                PaymentMethodId = 1,   // Default (e.g., Online)
                StatusId = 1,          // Default status (e.g., Completed)
                TransactionDateTime = DateTime.UtcNow,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var savedTransaction = await _transactionRepository.AddAsync(transaction);

            // Step 3: Create Order
            var order = new TblOrder
            {
                UserId = userId,
                OrderStatus = "Completed",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = userId.ToString(),
                TransactionId = savedTransaction.TransactionId,
                TransactionNumber = savedTransaction.TransactionNumber,
                TblOrderAssets = assetIds.Select(id => new TblOrderAsset
                {
                    AssetId = id
                }).ToList()
            };

            _context.TblOrders.Add(order);
            await _context.SaveChangesAsync();

            // Map the created order to DirectSaleAssetDto
            var assetDtos = order.TblOrderAssets
                .Where(oa => oa.Asset != null)
                .Select(oa =>
                {
                    var dto = _mapper.Map<DirectSaleAssetDto>(oa.Asset);
                    return dto;
                })
                .ToList();

            foreach (var id in assetIds)
            {
                // remove from this user's cart
                await _cartRepository.RemoveFromCartAsync(userId, id);
                var asset = await _assetsRepository.GetIdDeleteAsync(id);
                await _assetsRepository.DeleteAsync(asset);
            }


            return assetDtos;
        }




    }
}
