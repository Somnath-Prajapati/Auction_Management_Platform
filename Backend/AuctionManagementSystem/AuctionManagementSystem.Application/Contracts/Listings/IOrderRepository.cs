using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;

namespace AuctionManagementSystem.Application.Contracts.Listings
{
    public interface IOrderRepository
    {
        Task<List<DirectSaleAssetDto>> GetAllOrders(int userId);

    }
}
