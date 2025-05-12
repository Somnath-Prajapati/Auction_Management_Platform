using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AuctionManagementDbContext _context;

        public WishlistRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(AddToWishlistDto dto)
        {
            var exists = await _context.TblWishlistItems
                .FirstOrDefaultAsync(w => w.UserId == dto.UserId && w.AssetId == dto.AssetId && w.DeletedDate == null);

            if (exists != null)
                return false;

            var entity = new TblWishlistItem
            {
                UserId = dto.UserId,
                AssetId = dto.AssetId,
                AddedAt = DateTime.UtcNow
            };

            await _context.TblWishlistItems.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<DirectSaleAssetDto>> GetWishlistByUserIdAsync(int userId)
        {
            return await _context.TblWishlistItems
                .Where(w => w.UserId == userId && w.DeletedDate == null)
                .Include(w => w.Asset).ThenInclude(a => a.Category)
                .Include(w => w.Asset.TblAssetGalleries)
                .Select(w => new DirectSaleAssetDto
                {
                    AssetId = w.Asset.AssetId,
                    Title = w.Asset.Title,
                    CategoryId = w.Asset.CategoryId,
                    CategoryName = w.Asset.Category != null ? w.Asset.Category.CategoryName : null,
                    Deposit = w.Asset.Deposit,
                    MinIncrement = w.Asset.MinIncrement,
                    Description = w.Asset.Description,
                    IsActive = w.Asset.IsActive,
                    SalesNotes = w.Asset.SalesNotes,
                    Price = w.Asset.StartingPrice,
                    IsAvailableForDirectSale = w.Asset.IsAvailableForDirectSale,
                    ThumbnailUrl = w.Asset.TblAssetGalleries
                        .OrderBy(g => g.SortOrder)
                        .Select(g => g.FilePath)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

    }

}
