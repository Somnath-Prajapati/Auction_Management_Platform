using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AuctionManagementSystem.Persistence.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AuctionRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblAuction>> GetAllAsync()
        {
            // Step 1: Load auctions with related data
            var auctions = await _context.TblAuctions
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.TblAuctionAssets)
                    .ThenInclude(aa => aa.Asset)
                .Where(a => !a.IsDeleted)
                .ToListAsync();

            // Step 2: Get auction IDs
            var auctionIds = auctions.Select(a => a.AuctionId).ToList();

            // Step 3: Query total bid amounts per auction from TblBid
            var totalBidsPerAuction = await (
                from b in _context.tblBids
                join a in _context.TblAuctions on b.AuctionId equals a.AuctionId
                where !a.IsDeleted
                group b by b.AuctionId into g
                select new
                {
                    AuctionId = g.Key,
                    Total = g.Sum(b => b.BidAmount)
                }
            ).ToDictionaryAsync(x => x.AuctionId, x => x.Total);

            foreach (var auction in auctions)
            {
                auction.TotalPrice = totalBidsPerAuction.TryGetValue(auction.AuctionId, out var total)
                    ? total
                    : 0m;
            }

            return auctions;
        }



        public async Task<TblAuction> GetByIdAsync(int id)
        {   
            return await _context.TblAuctions
                .Where(a => a.AuctionId == id && !a.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(TblAuction auction)
        {
            auction.AuctionNumber = await GenerateNextAuctionNumberAsync();
            await _context.TblAuctions.AddAsync(auction);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GenerateNextAuctionNumberAsync(string prefix = "AUC", int startFrom = 68)
        {
            var lastNumber = await _context.TblAuctions
                .Where(a => !a.IsDeleted && a.AuctionNumber.StartsWith(prefix))
                .Select(a => a.AuctionNumber)
                .ToListAsync();

            int maxNumeric = lastNumber
                .Select(n => {
                    var numPart = n.Substring(prefix.Length);
                    return int.TryParse(numPart, out var result) ? result : 0;
                })
                .DefaultIfEmpty(startFrom - 1)
                .Max();

            var newAuctionNumber = $"{prefix}{(maxNumeric + 1).ToString("D5")}";

            Console.WriteLine($"Generated Auction Number: {newAuctionNumber}"); // ✅ Log here

            return newAuctionNumber;
        }



        public void Update(TblAuction auction)
        {
            _context.TblAuctions.Update(auction);
        }

        public void Delete(TblAuction auction)
        {
            auction.IsDeleted = true;
            _context.TblAuctions.Update(auction);
        }

    }
}
