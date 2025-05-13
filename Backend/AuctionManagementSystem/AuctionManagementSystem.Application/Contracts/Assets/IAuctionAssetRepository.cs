using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Auction;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAuctionAssetRepository
    {

        Task<IEnumerable<int>> GetAssignedAuctionIdsAsync(int assetId);
        Task AddAsync(TblAuctionAsset auctionAsset);
        Task<List<TblAuction>> GetAllAuctionsAsync();
        Task<bool> AssetExistsInAuctionAsync(int auctionId, int assetId);
    }
}
