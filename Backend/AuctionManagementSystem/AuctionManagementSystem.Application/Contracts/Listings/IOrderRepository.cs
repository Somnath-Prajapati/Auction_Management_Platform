using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Listings
{
    public interface IOrderRepository
    {
        Task<List<DirectSaleAssetDto>> GetAllOrders(int userId);
        //Task<List<DirectSaleAssetDto>> ConfirmPaymentAndCreateOrderAsync(int userId, List<int> assetIds,string payment);
        Task<List<DirectSaleAssetDto>> ConfirmPaymentAndCreateOrderAsync(int userId, List<int> assetIds, string payment, long amountpaid);

    }
}
