using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public interface ICartRepository
    {
        Task<bool> RemoveFromCartAsync(int userId, int assetId);
        Task<List<DirectSaleAssetDto>> GetCartByUserIdAsync(int userId);

        Task<bool> AddToCartAsync(int userId, int assetId);

        //Task<bool> AddOrUpdateCartItemAsync(int userId, int assetId, int quantity);

        Task<bool> DecreaseCartItemQuantityAsync(int userId, int assetId);

        Task<bool> ExistsAsync(int userId, int assetId);  // Add this method signature

    }
}
