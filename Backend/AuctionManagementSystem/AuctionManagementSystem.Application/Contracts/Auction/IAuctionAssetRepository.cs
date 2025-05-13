using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Contracts.Auction
{
    public interface IAuctionAssetRepository
    {
        Task<bool> AssetExistsInAuctionAsync(int auctionId, int assetId);
    }
}
