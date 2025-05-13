using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AuctionManagementDbContext _context;

        public WishlistRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> AddAsync(AddToWishlistDto dto)
        //{
        //    var wishlistItem = new TblWishlistItem
        //    {
        //        UserId = dto.UserId,
        //        AssetId = dto.AssetId,
        //        AddedAt = DateTime.UtcNow,
        //        IsActive = true
        //    };

        //    await _context.TblWishlistItems.AddAsync(wishlistItem);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> AddToWishlistAsync(int userId, int assetId)
        {
            var alreadyExists = await ExistsAsync(userId, assetId);
            if (alreadyExists)
                return false;

            var wishlistItem = new TblWishlistItem
            {
                UserId = userId,
                AssetId = assetId,
                AddedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.TblWishlistItems.AddAsync(wishlistItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int assetId)
        {
            var wishlistItem = await _context.TblWishlistItems
                .FirstOrDefaultAsync(w => w.UserId == userId && w.AssetId == assetId && w.IsActive);

            if (wishlistItem != null)
            {
                wishlistItem.IsActive = false;
                wishlistItem.DeletedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<DirectSaleAssetDto>> GetWishlistAssetsByUserIdAsync(int userId)
        {
            return await _context.TblWishlistItems
                .Where(w => w.UserId == userId && w.IsActive)
                .Include(w => w.Asset)
                    .ThenInclude(a => a.TblAssetGalleries)
                .Include(w => w.Asset.Category)
                .Select(w => new DirectSaleAssetDto
                {
                    AssetId = w.Asset.AssetId,
                    Title = w.Asset.Title,
                    CategoryId = w.Asset.CategoryId,
                    Deposit = w.Asset.Deposit,
                    MinIncrement = w.Asset.MinIncrement,
                    Description = w.Asset.Description,
                    IsActive = w.Asset.IsDeleted,
                    SalesNotes = w.Asset.SalesNotes,
                    Price = w.Asset.StartingPrice,
                    IsAvailableForDirectSale = w.Asset.IsAvailableForDirectSale,
                    CategoryName = w.Asset.Category.CategoryName,
                    ThumbnailUrl = w.Asset.TblAssetGalleries
                        .Where(g => g.SortOrder == 1)
                        .Select(g => g.FilePath)
                        .FirstOrDefault() ?? string.Empty
                })
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int userId, int assetId)
        {
            return await _context.TblWishlistItems
                .AnyAsync(w => w.UserId == userId && w.AssetId == assetId && w.IsActive);
        }
    }
}
