using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Application.Services
{
    public class AuctionWinnerService : IAuctionWinnerService
    {
        private readonly AuctionManagementDbContext _context;
        private readonly IWinnerNotificationService _notificationService;
        private readonly IAssetWinnerRepository _assetWinnerRepository;
        public AuctionWinnerService(AuctionManagementDbContext context, IWinnerNotificationService notificationService, IAssetWinnerRepository assetWinnerRepository)
        {
            _context = context;
            _notificationService = notificationService;
            _assetWinnerRepository = assetWinnerRepository;
        }

        public async Task<List<AuctionWinnerDto>> WinnerAuctionAsync(int auctionId)
        {
            var auction = await _context.TblAuctions.FindAsync(auctionId);
            if (auction == null)
            {
                throw new NotFoundException("No valid Auction Found");
            }

            auction.StatusId = 3;

            var winners = await _context.tblBids.Where(b => b.AuctionId == auctionId && b.IsWinningBid)
                .Select(b => new AuctionWinnerDto(b.AuctionId,
                b.AssetId,
                b.UserId,
                b.BidAmount)).ToListAsync();

            foreach (var winner in winners)
            {
                var existingWinner = await _context.TblAssetWinners.FirstOrDefaultAsync(w => w.AssetId == winner.AssetId);

                if (existingWinner == null)
                {
                    var assetWinner = new TblAssetWinner
                    {
                        AssetId = winner.AssetId,
                        UserId = winner.UserId,
                        AwardedPrice = winner.BidAmount,
                        Reason = "Top Bidder",
                        Note = "All checks completed",
                        Approved = true,
                        CreatedAt = DateTime.Now
                    };

                    await _assetWinnerRepository.AddAsync(assetWinner);
                    await _context.SaveChangesAsync();


                    var asset = await _context.TblAssets.FirstOrDefaultAsync(a => a.AssetId == winner.AssetId);
                    if (asset != null)
                    {
                        asset.WinnerId = assetWinner.WinnerId;
                        _context.TblAssets.Update(asset);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            await _context.SaveChangesAsync();

            Console.WriteLine("Auction Winners:");
            foreach (var winner in winners)
            {
                Console.WriteLine($"AuctionId: {winner.AuctionId}, AssetId: {winner.AssetId}, UserId: {winner.UserId}, BidAmount: {winner.BidAmount}");
            }



            await _notificationService.NotifyWinnerList(winners);
            return winners;
        }
    }
}
