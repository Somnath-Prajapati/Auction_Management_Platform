using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Dtos.Assets;
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

        public async Task<IEnumerable<TblAutoBid>> GetActiveAutoBidsForAssetAsync(int auctionId, int assetId)
        {
            return await _context.TblAutoBids
                .Where(ab => ab.AuctionId == auctionId &&
                             ab.AssetId == assetId &&
                             ab.MaxBidAmount > 0 &&
                             ab.IsActive) 
                .OrderBy(ab => ab.CreatedDate) 
                .ToListAsync();
        }

        //public async Task<List<(int auctionId, int assetId)>> GetActiveAuctionAssetPairsAsync()
        //{
        //    return await _context.TblAuctionAssets
        //        .Where(aa => aa.Auction.StatusId == 2 && aa.Auction.EndDateTime > DateTime.UtcNow)
        //        .Select(aa => new ValueTuple<int, int>(aa.AuctionId, aa.AssetId))
        //        .ToListAsync();
        //}

        public async Task<List<(int auctionId, int assetId)>> GetActiveAuctionAssetPairsAsync()
        {
            var pairs = new List<(int auctionId, int assetId)>(10000); 


            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "[AuctionM_dbuser].[spGetActiveAuctionAssetPairsWithActiveAutoBids]";
            command.CommandType = CommandType.StoredProcedure;

            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var auctionId = reader.GetInt32(0); // assuming column 0 = AuctionId
                var assetId = reader.GetInt32(1);   // assuming column 1 = AssetId

                pairs.Add((auctionId, assetId));
            }

            return pairs;
        }


        public async Task<tblBid?> GetHighestBidAsync(int assetId)
        {
            var ans = await _context.tblBids
                .Where(b=>b.AssetId == assetId)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefaultAsync();

            return ans;
        }

        public async Task ExtendAuctionIfCloseToEndAsync(int auctionId, DateTime currentTime)
        {
            var auction = await _context.TblAuctions.FindAsync(auctionId);
            if (auction == null) return;

            if (auction.EndDateTime > currentTime && auction.EndDateTime - currentTime <= TimeSpan.FromMinutes(5))
            {
                auction.EndDateTime = currentTime.AddMinutes(5);
                _context.TblAuctions.Update(auction); 
                await _context.SaveChangesAsync();
            }
        }

       
    }
}
