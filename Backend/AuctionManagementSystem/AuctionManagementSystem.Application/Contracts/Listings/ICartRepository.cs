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
        Task<bool> AddAsync(AddToCartDto dto);
        Task<List<DirectSaleAssetDto>> GetCartByUserIdAsync(int userId);
    }
}
