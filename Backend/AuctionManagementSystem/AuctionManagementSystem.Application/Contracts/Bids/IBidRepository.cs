using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Bids;

namespace AuctionManagementSystem.Application.Contracts.Bids
{
    public interface IBidRepository
    {
        Task<int> AddBidAsync(tblBid bid);
        Task<tblBid?> GetWinningBidAsync(int assetId); 
        Task<IEnumerable<tblBid>> GetBidsByAssetIdAsync(int assetId);
        Task<IEnumerable<tblBid>> GetBidsByUserIdAsync(int UserId);

        Task<decimal?> GetHighestBidAmountAsync(int assetId);
        Task UnsetPreviousWinningBidAsync(int assetId);
        Task<(decimal HighestBid, int BidCount)> GetBidStatsByAssetIdAsync(int assetId);
        Task<int> CountBidsByAssetIdAsync(int assetId);

    }

}
