using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Domain.Models;
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
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserId == bid.UserId);
            if (user == null || user.AvailableLimit < bid.BidAmount)
                throw new InvalidOperationException("Insufficient limit.");
        
            var auditLog = new TblUserLimitAuditLog
            {
                UserId = user.UserId,
                ActionType = "Bid",
                OldDeposit = user.Deposit,
                NewDeposit = user.Deposit,
                OldTotalLimit = user.TotalLimit,
                NewTotalLimit = user.TotalLimit,
                OldAvailableLimit = user.AvailableLimit,
                NewAvailableLimit = user.AvailableLimit-bid.BidAmount,
                Notes = $"available limit changed because of Bid",
                ChangedBy = user.UserId
            };
            // Deduct the bid amount
            user.AvailableLimit -= bid.BidAmount;

            _context.TblUserLimitAuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();


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
        public async Task<IEnumerable<tblBid>> GetBidsByUserIdAsync(int userId)
        {
            return await (
                from b in _context.tblBids
                join a in _context.TblAssets on b.AssetId equals a.AssetId
                where b.UserId == userId && !a.IsDeleted
                orderby b.BidTime descending
                select b
            ).ToListAsync();
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
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserId == bid.UserId);
            if (user == null || user.AvailableLimit < bid.BidAmount)
                throw new InvalidOperationException("Insufficient limit.");
            // Deduct the bid amount
            user.AvailableLimit -= bid.BidAmount;
            // Update the bid
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
            return await (
                from w in _context.TblAssetWinners
                join a in _context.TblAssets on w.AssetId equals a.AssetId
                where w.UserId == userId && !a.IsDeleted
                orderby w.CreatedAt descending
                select w
            ).ToListAsync();
        }




    }

}
