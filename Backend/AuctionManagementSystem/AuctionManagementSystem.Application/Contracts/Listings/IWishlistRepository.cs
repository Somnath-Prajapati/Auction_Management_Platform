using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public interface IWishlistRepository
    {
        Task<bool> AddToWishlistAsync(int userId, int assetId);

        //Task<bool> AddAsync(AddToWishlistDto dto);
        Task<bool> RemoveFromWishlistAsync(int userId, int assetId);
        Task<List<DirectSaleAssetDto>> GetWishlistAssetsByUserIdAsync(int userId);

        Task<bool> ExistsAsync(int userId, int assetId);  // Add this method signature

    }
}
