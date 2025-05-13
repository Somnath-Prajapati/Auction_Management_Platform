using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AuctionManagementDbContext _context;

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

        public async Task<bool> AddToCartAsync(int userId, int assetId)
        {
            // Check if the asset is already in the cart for this user
            var existingCartItem = await _context.TblCartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.AssetId == assetId && c.IsActive);

            // If the item already exists in the cart, return false (not adding it again)
            if (existingCartItem != null)
            {
                return false;
            }

            // If the item doesn't exist, create a new cart item
            var newCartItem = new TblCartItem
            {
                UserId = userId,
                AssetId = assetId,
                AddedAt = DateTime.UtcNow,
                IsActive = true
            };

            // Add to the cart
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
            return await _context.TblCartItems
                .Where(c => c.UserId == userId && c.IsActive)
                .Include(c => c.Asset)
                .ThenInclude(a => a.TblAssetGalleries) // Include the AssetGalleries related to Asset
                .Select(c => new DirectSaleAssetDto
                {
                    AssetId = c.Asset.AssetId,
                    Title = c.Asset.Title,
                    Price = c.Asset.StartingPrice,
                    Description = c.Asset.Description,
                    ThumbnailUrl = c.Asset.TblAssetGalleries
                                      .Where(g => g.SortOrder == 1)
                                      .Select(g => g.FilePath)
                                      .FirstOrDefault() ?? string.Empty, // Handle null reference explicitly
                })
                .ToListAsync();
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


    }
}
