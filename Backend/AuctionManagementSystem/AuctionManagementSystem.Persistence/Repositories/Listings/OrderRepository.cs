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
using AuctionManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    class OrderRepository : IOrderRepository
    {
        public readonly AuctionManagementDbContext _context;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IOrderEmailService _orderEmailService;
        private readonly ICartRepository _cartRepository;
        private readonly IAssetsRepository _assetsRepository;
        private readonly string _baseUrl = "https://localhost:62627/";
        private readonly IMapper _mapper;


        public OrderRepository(AuctionManagementDbContext context, ITransactionRepository transactionRepository, IMapper mapper, ICartRepository cartRepository,IAssetsRepository assetsRepository, IOrderEmailService orderEmailService)
        {
            _context
                = context;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _assetsRepository = assetsRepository;
            _orderEmailService = orderEmailService;
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

            // Preload all AssetWinners for this user in one go (to avoid per-asset DB hits)

            var userWinners = await _context.TblAssetWinners

                .Where(w => w.UserId == userId)

                .ToListAsync();

            // Preload all AssetWinners for this user in one go (to avoid per-asset DB hits)
         

            var assetDtos = orderItems
                .SelectMany(order => order.TblOrderAssets)
                .Where(oa => oa.Asset != null)
                .Select(oa =>
                {
                    var asset = oa.Asset;
                    var galleries = asset.TblAssetGalleries ?? new List<TblAssetGallery>();

                    var thumbnailPath = galleries
                        .OrderBy(g => g.SortOrder) // Optional if you have SortOrder
                        .Select(g => g.FilePath)

                        .FirstOrDefault();

                    // Determine price
                    decimal price = asset.IsAvailableForDirectSale
                        ? asset.StartingPrice
                        : userWinners.FirstOrDefault(w => w.AssetId == asset.AssetId)?.AwardedPrice ?? 0;

                    return new DirectSaleAssetDto

                    {
                        AssetId = asset.AssetId,
                        Title = asset.Title,
                        CategoryId = asset.CategoryId,
                        Deposit = asset.Deposit,
                        MinIncrement = asset.MinIncrement,
                        Description = asset.Description,
                        IsDeleted = asset.IsDeleted,
                        SalesNotes = asset.SalesNotes,
                        Price = price,
                        IsAvailableForDirectSale = asset.IsAvailableForDirectSale,
                        CategoryName = asset.Category?.CategoryName ?? string.Empty,
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

        public async Task<List<DirectSaleAssetDto>> ConfirmPaymentAndCreateOrderAsync(int userId, List<int> assetIds, string paymentMethod,long amountpaid)
        {
            if (assetIds == null || !assetIds.Any())
                throw new ArgumentException("No assets provided for the order.");

            // Step 1: Load Assets
            var assets = new List<TblAsset>();

            foreach (var id in assetIds)
            {
                var asset = await _context.TblAssets
                    .Include(a => a.TblAssetGalleries)
                    .Include(a => a.Category)
                    .FirstOrDefaultAsync(a => a.AssetId == id);

                if (asset != null)
                {
                    assets.Add(asset);
                }
            }


            if (assets.Count != assetIds.Count)
                throw new InvalidOperationException("Some assets were not found in the database.");

            // Step 2: Calculate Total
            var totalAmount = amountpaid;

            // Step 3: Map Payment Method String to ID
            var paymentMethodId = await _context.TblPaymentMethods
                .Where(p => p.PaymentMethodName.ToLower() == paymentMethod.ToLower())
                .Select(p => p.PaymentMethodId)
                .FirstOrDefaultAsync();

            if (paymentMethodId == 0)
                throw new InvalidOperationException($"Unknown payment method: {paymentMethod}");

            // Step 4: Create Transaction
            var transaction = new TblTransaction
            {
                UserId = userId,
                Amount = totalAmount,
                TransactionTypeId = 1, // Direct Sale
                PaymentMethodId = paymentMethodId,
                StatusId = 1, // Completed
                TransactionDateTime = DateTime.UtcNow,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };

            var savedTransaction = await _transactionRepository.AddAsync(transaction);

            // Step 5: Create Order
            var order = new TblOrder
            {
                UserId = userId,
                OrderStatus = "Completed",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = userId.ToString(),
                TransactionId = savedTransaction.TransactionId,
                TransactionNumber = savedTransaction.TransactionNumber,
                TblOrderAssets = assetIds.Select(id => new TblOrderAsset { AssetId = id }).ToList()
            };

            _context.TblOrders.Add(order);
            await _context.SaveChangesAsync();

            // Step 6: Remove from Cart and Delete Asset
            foreach (var asset in assets)
            {
                await _cartRepository.RemoveFromCartAsync(userId, asset.AssetId);
                await _assetsRepository.DeleteAsync(asset);
            }

            // Step 7: Map and Return DTOs
            var assetDtos = assets.Select(a => _mapper.Map<DirectSaleAssetDto>(a)).ToList();
            // Step 8: Send Confirmation Email
            //await _orderEmailService.SendOrderConfirmationEmailAsync(userId, savedTransaction, assets);

            return assetDtos;
        }

    }
}
