using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;

namespace AuctionManagementSystem.Persistence.Repositories.Listings
{
    public interface IWishlistRepository
    {
        Task<bool> AddAsync(AddToWishlistDto dto);
        Task<List<DirectSaleAssetDto>> GetWishlistByUserIdAsync(int userId);
    }
}
