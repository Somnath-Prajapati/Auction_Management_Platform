using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Bids;
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
        public async Task<IEnumerable<tblBid>> GetBidsByUserIdAsync(int UserId)
        {
            return await _context.tblBids
                .Where(b => b.UserId == UserId)
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
                .Where(b => b.AssetId == assetId && b.IsWinningBid)
                .ToListAsync();

            //if (currentWinningBid != null)
            //{
            //    currentWinningBid.IsWinningBid = false;
            //    _context.tblBids.Update(currentWinningBid);

            if (currentWinningBid == null || !currentWinningBid.Any())
                return;

           

            
            if(currentWinningBid.Count > 1)
            {
                foreach (var bid in currentWinningBid)
                {
                    bid.IsWinningBid = false;
                    _context.tblBids.Update(bid);   
                }
                }
                else
                {
                    currentWinningBid[0].IsWinningBid = false;
                    _context.tblBids.Update(currentWinningBid[0]);
                }
           
            
            await _context.SaveChangesAsync();
        }
        public async Task<(decimal HighestBid, int BidCount)> GetBidStatsByAssetIdAsync(int assetId)
        {
            var bids = _context.tblBids.Where(b => b.AssetId == assetId);

            var winningBid = await bids
              .Where(b => b.IsWinningBid)
              .FirstOrDefaultAsync();
            var highestBid = winningBid?.BidAmount ?? 0;
            var bidCount = await bids.CountAsync();

            return (highestBid, bidCount);
        }
        public async Task<int> CountBidsByAssetIdAsync(int assetId)
        {
            return await _context.tblBids.Where(b => b.AssetId == assetId).CountAsync();
        }


        public async Task<tblBid?> GetUserBidAsync(int userId, int auctionId, int assetId)
        {
               var result = await _context.tblBids
                .FirstOrDefaultAsync(b => b.UserId == userId && b.AuctionId == auctionId && b.AssetId == assetId && !b.IsAutoBid);
            return result;
        }

    
        public async Task UpdateBidAsync(tblBid bid)
        {
            _context.tblBids.Update(bid);
            await _context.SaveChangesAsync();
        }
        public async Task<tblBid?> GetWinningBidByAssetIdAsync(int assetId)
        {
            return await _context.tblBids
                .Where(b => b.AssetId == assetId && b.IsWinningBid == true)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TblAssetWinner>> GetWonBidsByUserIdAsync(int userId)
        {
            return await _context.TblAssetWinners
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
        }



    }

}
