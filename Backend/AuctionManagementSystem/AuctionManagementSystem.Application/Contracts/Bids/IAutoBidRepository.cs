using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Bids;
using Microsoft.AspNetCore.Identity;



namespace AuctionManagementSystem.Application.Contracts.Bids
{
    public interface IAutoBidRepository
    {
        Task<int> AddAutoBidAsync(TblAutoBid autoBid);
        Task<TblAutoBid?> GetUserAutoBidAsync(int userId, int auctionId, int assetId); 
        Task<IEnumerable<TblAutoBid>> GetAutoBiddersForAsset(int auctionId, int assetId);
        Task UpdateAutoBidAsync(TblAutoBid autoBid); 

        Task<TblAutoBid?> GetByUserAuctionAssetAsync(int userId, int auctionId, int assetId);
        Task<IEnumerable<TblAutoBid>> GetActiveAutoBidsForAssetAsync(int auctionId, int assetId);


        Task<List<(int auctionId, int assetId)>> GetActiveAuctionAssetPairsAsync();
        Task<tblBid?> GetHighestBidAsync(int assetId);
        Task ExtendAuctionIfCloseToEndAsync(int auctionId, DateTime currentTime);




    }

}
