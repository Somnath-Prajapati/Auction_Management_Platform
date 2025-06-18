using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Services
{
    public class AuctionWinnerService : IAuctionWinnerService
    {
        private readonly AuctionManagementDbContext _context;
        private readonly IWinnerNotificationService _notificationService;
        private readonly IAssetWinnerRepository _assetWinnerRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;
        private readonly INotificationRepository _notificationRepository;
        private readonly ICartRepository _CartRepository;
        public AuctionWinnerService(AuctionManagementDbContext context, IWinnerNotificationService notificationService, IAssetWinnerRepository assetWinnerRepository, INotificationBroadcaster notificationBroadcaster, INotificationRepository notificationRepository, ICartRepository cartRepository)
        {
            _context = context;
            _notificationService = notificationService;
            _assetWinnerRepository = assetWinnerRepository;
            _notificationBroadcaster = notificationBroadcaster;
            _notificationRepository = notificationRepository;
            _CartRepository = cartRepository;
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
                    var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserId == winner.UserId);
                    if (asset != null)
                    {
                        asset.WinnerId = assetWinner.WinnerId;
                        _context.TblAssets.Update(asset);
                        await _context.SaveChangesAsync();
                    }

                    var notification = new TblNotification
                    {
                        NotificationId = Guid.NewGuid(),
                        UserId = null,
                        Title = $"Winner Announced for Asset :: {asset.Title}",
                        Message = $"Asset'{winner.AssetId}' been won by {user.Name}.",
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(2),
                        IsRead = false,
                        AssetId = winner.AssetId,
                        AuctionId = winner.AuctionId
                    };
                    await _notificationRepository.CreateAsync(notification);
                    var UserNotification = new TblNotification
                    {
                        NotificationId = Guid.NewGuid(),
                        UserId = winner.UserId,
                        Title = $"Winner Announced for Asset :: {asset.Title}",
                        Message = $"Asset'{winner.AssetId}' been won by you.",
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(2),
                        IsRead = false,
                        AssetId = winner.AssetId,
                        AuctionId = winner.AuctionId
                    };
                    await _notificationRepository.CreateAsync(UserNotification);
                    var notificationDto = new NotificationDto
                    {
                        UserId = notification.UserId,
                        Title = notification.Title,
                        Message = notification.Message,
                        ExpiresAt = notification.ExpiresAt,
                        AuctionId = notification.AuctionId,
                        AssetId = notification.AssetId,
                    };

                    await _notificationBroadcaster.BroadcastNotificationAsync(notificationDto);
                    var UserNotificationDto = new NotificationDto
                    {
                        UserId = UserNotification.UserId,
                        Title = UserNotification.Title,
                        Message = UserNotification.Message,
                        ExpiresAt = UserNotification.ExpiresAt,
                        AuctionId = UserNotification.AuctionId,
                        AssetId = UserNotification.AssetId,
                    };

                    await _notificationBroadcaster.NotifyByUserId(UserNotificationDto);
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
