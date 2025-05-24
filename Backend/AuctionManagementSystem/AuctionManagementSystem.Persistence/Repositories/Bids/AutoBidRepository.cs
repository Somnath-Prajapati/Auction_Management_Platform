using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Bids
{
    public class AutoBidRepository : IAutoBidRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AutoBidRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAutoBidAsync(TblAutoBid autoBid)
        {
            _context.TblAutoBids.Add(autoBid);
            await _context.SaveChangesAsync();
            return autoBid.AutoBidId;
        }

        public async Task<IEnumerable<TblAutoBid>> GetAutoBiddersForAsset(int auctionId, int assetId)
        {
            return await _context.TblAutoBids
                .Where(x => x.AuctionId == auctionId && x.AssetId == assetId && x.IsActive)
                .ToListAsync();
        }

        public async Task<TblAutoBid?> GetByUserAuctionAssetAsync(int userId, int auctionId, int assetId)
        {
            return await _context.TblAutoBids   
       .FirstOrDefaultAsync(x =>
           x.UserId == userId &&
           x.AuctionId == auctionId &&
           x.AssetId == assetId &&
           x.IsActive); 
        }

        public async Task<TblAutoBid?> GetUserAutoBidAsync(int userId, int auctionId, int assetId)
        {
            return await _context.TblAutoBids
                .FirstOrDefaultAsync(x => x.UserId == userId && x.AuctionId == auctionId && x.AssetId == assetId);
        }

        public async Task UpdateAutoBidAsync(TblAutoBid autoBid)
        {
            _context.TblAutoBids.Update(autoBid);
            await _context.SaveChangesAsync();
        }
    }
}
