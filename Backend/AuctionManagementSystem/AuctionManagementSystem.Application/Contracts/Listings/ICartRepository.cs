using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Domain.Entities;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public interface ICartRepository
    {
        Task<bool> RemoveFromCartAsync(int userId, int assetId);
        Task<List<DirectSaleAssetDto>> GetCartByUserIdAsync(int userId);

        Task<bool> AddToCartAsync(int userId, int assetId, int validMinutes);

        //Task<bool> AddOrUpdateCartItemAsync(int userId, int assetId, int quantity);

        Task<bool> DecreaseCartItemQuantityAsync(int userId, int assetId);

        Task<bool> ExistsAsync(int userId, int assetId);  // Add this method signature
        Task<bool> ExistsAsync(int userId, int assetId, int validMinutes);

        Task<bool> IsAssetHeldByAnotherUserAsync(int userId, int assetId, int validMinutes);
        Task<List<TblCartItem>> GetExpiredCartItemsAsync(int userId, int validMinutes);
        Task<List<DirectSaleAssetDto>> GetValidCartItemsAsync(int userId, int validMinutes);
        Task<List<TblCartItem>> GetAllValidCartItemsAsync(int validMinutes);
        Task UpdateAsync(TblCartItem item);
        Task CleanUpExpiredCartItemsAsync(int validMinutes);


        Task UpdateRangeAsync(IEnumerable<TblCartItem> items);



    }
}
