using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AuctionManagementSystem.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AuctionManagementDbContext _context;
        private readonly string _baseUrl = "https://localhost:62627/";

        public CartRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> AddOrUpdateCartItemAsync(int userId, int assetId, int quantity)
        //{
        //    var existing = await _context.TblCartItems
        //        .FirstOrDefaultAsync(c => c.UserId == userId && c.AssetId == assetId && c.IsActive);

        //    if (existing != null)
        //    {
        //        existing.Quantity += quantity;  // increment quantity
        //        existing.AddedAt = DateTime.UtcNow;
        //    }
        //    else
        //    {
        //        var cartItem = new TblCartItem
        //        {
        //            UserId = userId,
        //            AssetId = assetId,
        //            Quantity = quantity,
        //            AddedAt = DateTime.UtcNow,
        //            IsActive = true
        //        };
        //        await _context.TblCartItems.AddAsync(cartItem);
        //    }

        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> AddToCartAsync(int userId, int assetId, int validMinutes)
        {
            var now = DateTime.UtcNow;

            // Remove expired cart items for this user
            var expiredItems = await _context.TblCartItems
                .Where(c => c.UserId == userId && c.IsActive && c.AddedAt.AddMinutes(validMinutes) < now)
                .ToListAsync();

            foreach (var expired in expiredItems)
            {
                expired.IsActive = false;
                expired.DeletedDate = now;
                _context.TblCartItems.Update(expired);
            }

            // Save removals before proceeding
            await _context.SaveChangesAsync();

            // Check if user already has the asset in cart (and it's valid)
            var existing = await _context.TblCartItems
                .FirstOrDefaultAsync(c =>
                    c.UserId == userId &&
                    c.AssetId == assetId &&
                    c.IsActive &&
                    c.AddedAt.AddMinutes(validMinutes) > now);

            if (existing != null)
                return false;

            

            bool heldByAnother = await IsAssetHeldByAnotherUserAsync(userId, assetId, validMinutes);
            //bool heldByAnother = await _context.TblCartItems
            //    .AnyAsync(c =>
            //        c.UserId != userId &&
            //        c.AssetId == assetId &&
            //        c.IsActive &&
            //        c.AddedAt.AddMinutes(validMinutes) > now);

            if (heldByAnother)
            {
                throw new Exception("Asset is Already Held by another user Check after" + validMinutes + "minutes");
                //return false;
            }


            // Add to cart
            var newCartItem = new TblCartItem
            {
                UserId = userId,
                AssetId = assetId,
                AddedAt = now,
                IsActive = true
            };

            await _context.TblCartItems.AddAsync(newCartItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int assetId)
        {
            var cartItem = await _context.TblCartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.AssetId == assetId && c.IsActive);

            if (cartItem != null)
            {
                cartItem.IsActive = false;
                cartItem.DeletedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<DirectSaleAssetDto>> GetCartByUserIdAsync(int userId)
        {
            var wishlistItems = await _context.TblWishlistItems
         .Where(w => w.UserId == userId && w.IsActive)
         .Include(w => w.Asset)
             .ThenInclude(a => a.TblAssetGalleries)
         .Include(w => w.Asset.Category)
         .ToListAsync();

            return wishlistItems.Select(w =>
            {
                // Get image with SortOrder = 1, fallback to first if not found
                var thumbnailPath = w.Asset.TblAssetGalleries?
                    .Where(g => g.SortOrder == 1)
                    .Select(g => g.FilePath)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(thumbnailPath))
                {
                    thumbnailPath = w.Asset.TblAssetGalleries?
                        .Select(g => g.FilePath)
                        .FirstOrDefault();
                }

                return new DirectSaleAssetDto
                {
                    AssetId = w.Asset.AssetId,
                    Title = w.Asset.Title,
                    CategoryId = w.Asset.CategoryId,
                    Deposit = w.Asset.Deposit,
                    MinIncrement = w.Asset.MinIncrement,
                    Description = w.Asset.Description,
                    IsDeleted = w.Asset.IsDeleted,
                    SalesNotes = w.Asset.SalesNotes,
                    Price = w.Asset.StartingPrice,
                    IsAvailableForDirectSale = w.Asset.IsAvailableForDirectSale,
                    CategoryName = w.Asset.Category?.CategoryName ?? string.Empty,
                    ThumbnailUrl = string.IsNullOrEmpty(thumbnailPath) ? string.Empty : $"{_baseUrl}{thumbnailPath}"
                };
            }).ToList();

        }

        public async Task<bool> DecreaseCartItemQuantityAsync(int userId, int assetId)
        {
            var cartItem = await _context.TblCartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.AssetId == assetId && c.IsActive);

            if (cartItem == null)
                return false;

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                cartItem.AddedAt = DateTime.UtcNow;
            }
            else
            {
                cartItem.IsActive = false;
                cartItem.DeletedDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int userId, int assetId)
        {
            return await _context.TblCartItems
                .AnyAsync(c => c.UserId == userId && c.AssetId == assetId && c.IsActive);
        }

        public async Task<bool> ExistsAsync(int userId, int assetId, int validMinutes)
        {
            var threshold = DateTime.UtcNow.AddMinutes(-validMinutes);

            return await _context.TblCartItems.AnyAsync(ci =>
                ci.UserId == userId &&
                ci.AssetId == assetId &&
                ci.IsActive &&
                (ci.DeletedDate == null) &&
                ci.AddedAt >= threshold);
        }


        public async Task<bool> IsAssetHeldByAnotherUserAsync(int userId, int assetId, int validMinutes)
        {
            var threshold = DateTime.UtcNow.AddMinutes(-validMinutes);

            return await _context.TblCartItems.AnyAsync(ci =>
                ci.UserId != userId &&
                ci.AssetId == assetId &&
                ci.IsActive &&
                (ci.DeletedDate == null) &&
                ci.AddedAt >= threshold);
        }


        public async Task<List<TblCartItem>> GetExpiredCartItemsAsync(int userId, int validMinutes)
        {
            var threshold = DateTime.UtcNow.AddMinutes(-validMinutes);

            return await _context.TblCartItems
                .Where(ci =>
                    ci.UserId == userId &&
                    ci.IsActive &&
                    (ci.DeletedDate == null) &&
                    ci.AddedAt < threshold)
                .ToListAsync();
        }


        public async Task<List<TblCartItem>> GetValidCartItemsAsync(int userId, int validMinutes)
        {
            var threshold = DateTime.UtcNow.AddMinutes(-validMinutes);

            return await _context.TblCartItems
                .Include(ci => ci.Asset) // include related asset
                .Where(ci =>
                    ci.UserId == userId &&
                    ci.IsActive &&
                    ci.DeletedDate == null &&
                    ci.AddedAt >= threshold)
                    .ToListAsync();
                }

        public Task<List<TblCartItem>> GetAllValidCartItemsAsync(int validMinutes)
        {
            var threshold = DateTime.UtcNow.AddMinutes(-validMinutes);
            return _context.TblCartItems
                .Where(c =>  c.AddedAt > threshold)
                .ToListAsync();
        }



        public async Task UpdateAsync(TblCartItem item)
        {
            _context.TblCartItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task CleanUpExpiredCartItemsAsync(int validMinutes)
        {
            var threshold = DateTime.UtcNow.AddMinutes(-validMinutes);

            var expiredItems = await _context.TblCartItems
                .Where(ci =>
                    ci.IsActive &&
                    ci.DeletedDate == null &&
                    ci.AddedAt < threshold)
                .ToListAsync();

            foreach (var item in expiredItems)
            {
                item.IsActive = false;
                item.DeletedDate = DateTime.UtcNow;
            }

            if (expiredItems.Any())
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TblCartItem> items)
        {
            _context.TblCartItems.UpdateRange(items);
            await _context.SaveChangesAsync();
        
        
        }

    }
}
