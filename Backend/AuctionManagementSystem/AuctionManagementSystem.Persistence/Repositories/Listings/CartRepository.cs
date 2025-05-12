using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public class CartRepository : ICartRepository
    {
        private readonly AuctionManagementDbContext _context;

        public CartRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(AddToCartDto dto)
        {
            var exists = await _context.TblCartItems
                .FirstOrDefaultAsync(c => c.UserId == dto.UserId && c.AssetId == dto.AssetId && c.DeletedDate == null);

            if (exists != null)
                return false;

            //var expirationTimeSetting = await _context.TblSystemSettings
            //    .Where(s => s.Key == "CartExpirationMinutes")
            //    .Select(s => int.Parse(s.Value))
            //    .FirstOrDefaultAsync();

            var entity = new TblCartItem
            {
                UserId = dto.UserId,
                AssetId = dto.AssetId,
                AddedAt = DateTime.UtcNow,
                //ExpiresAt = DateTime.UtcNow.AddMinutes(expirationTimeSetting > 0 ? expirationTimeSetting : 60)
            };

            await _context.TblCartItems.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<DirectSaleAssetDto>> GetCartByUserIdAsync(int userId)
        {
            return await _context.TblCartItems
                .Where(c => c.UserId == userId && c.DeletedDate == null)
                .Include(c => c.Asset).ThenInclude(a => a.Category)
                .Include(c => c.Asset.TblAssetGalleries)
                .Select(c => new DirectSaleAssetDto
                {
                    AssetId = c.Asset.AssetId,
                    Title = c.Asset.Title,
                    CategoryId = c.Asset.CategoryId,
                    CategoryName = c.Asset.Category != null ? c.Asset.Category.CategoryName : null,
                    Deposit = c.Asset.Deposit,
                    MinIncrement = c.Asset.MinIncrement,
                    Description = c.Asset.Description,
                    IsActive = c.Asset.IsActive,
                    SalesNotes = c.Asset.SalesNotes,
                    Price = c.Asset.StartingPrice,
                    IsAvailableForDirectSale = c.Asset.IsAvailableForDirectSale,
                    ThumbnailUrl = c.Asset.TblAssetGalleries
                        .OrderBy(g => g.SortOrder)
                        .Select(g => g.FilePath)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

    }

}
