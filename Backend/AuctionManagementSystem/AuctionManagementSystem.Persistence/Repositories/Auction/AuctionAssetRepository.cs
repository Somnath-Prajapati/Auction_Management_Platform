using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Auction;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Auction
{
    public class AuctionAssetRepository : IAuctionAssetRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AuctionAssetRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }
        public async  Task<bool> AssetExistsInAuctionAsync(int auctionId, int assetId)
        {
            return await _context.TblAuctionAssets
           .AnyAsync(x => x.AuctionId == auctionId && x.AssetId == assetId);
        }
    }
}
