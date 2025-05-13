using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Bids
{
    public class BidRepository : IBidRepository
    {
        private readonly AuctionManagementDbContext _context;

        public BidRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddBidAsync(tblBid bid)
        {
            _context.tblBids.Add(bid);
            await _context.SaveChangesAsync();
            return bid.BidId;
        }

        public async Task<tblBid?> GetWinningBidAsync(int assetId)
        {
            return await _context.tblBids
                .Where(b => b.AssetId == assetId && b.IsWinningBid)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<tblBid>> GetBidsByAssetIdAsync(int assetId)
        {
            return await _context.tblBids
                .Where(b => b.AssetId == assetId)
                .OrderByDescending(b => b.BidTime)
                .ToListAsync();
        }
        public async Task<decimal?> GetHighestBidAmountAsync(int assetId)
        {
            return await _context.tblBids
                .Where(b => b.AssetId == assetId)
                .MaxAsync(b => (decimal?)b.BidAmount);
        }
        public async Task UnsetPreviousWinningBidAsync(int assetId)
        {
            var currentWinningBid = await _context.tblBids
                .FirstOrDefaultAsync(b => b.AssetId == assetId && b.IsWinningBid);

            if (currentWinningBid != null)
            {
                currentWinningBid.IsWinningBid = false;
                _context.tblBids.Update(currentWinningBid);
            }
        }


    }

}
