using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Listings;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.AspNetCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    class OrderRepository : IOrderRepository
    {
        public readonly AuctionManagementDbContext _context;
        private readonly string _baseUrl = "https://localhost:62627/";

        public OrderRepository(AuctionManagementDbContext context)
        {
            _context
                = context;
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

    }
}
