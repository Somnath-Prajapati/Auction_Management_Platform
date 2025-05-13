using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Assets
{
    public class AuctionAssetRepository : IAuctionAssetRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AuctionAssetRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<int>> GetAssignedAuctionIdsAsync(int assetId)
        {
            return await _context.TblAuctionAssets
                .Where(x => x.AssetId == assetId)
                .Select(x => x.AuctionId)
                .ToListAsync();

        }

        public async Task AddAsync(TblAuctionAsset auctionAsset)
        {
            _context.TblAuctionAssets.Add(auctionAsset);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TblAuction>> GetAllAuctionsAsync()
        {
            return await _context.TblAuctions.ToListAsync();
        }
        public async Task<bool> AssetExistsInAuctionAsync(int auctionId, int assetId)
        {
            return await _context.TblAuctionAssets
           .AnyAsync(x => x.AuctionId == auctionId && x.AssetId == assetId);
        }
    }
}