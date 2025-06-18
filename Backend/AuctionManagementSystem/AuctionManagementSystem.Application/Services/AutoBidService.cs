using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Domain.Entities.Notification;

namespace AuctionManagementSystem.Application.Services
{
    public class AutoBidService : IAutoBidService
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IAutoBidRepository _autoBidRepository;
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IBidNotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;


        public AutoBidService(
            IAssetsRepository assetsRepository,
            IBidRepository bidRepository,
            IAutoBidRepository autoBidRepository,
            IUnitOfWorkAuth unitOfWork,
            IBidNotificationService notificationService,
            INotificationRepository notificationRepository,
            INotificationBroadcaster notificationBroadcaster)
        {
            _assetsRepository = assetsRepository;
            _bidRepository = bidRepository;
            _autoBidRepository = autoBidRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _notificationRepository = notificationRepository;
            _notificationBroadcaster = notificationBroadcaster;
        }

        public async Task RunAutoBidRoundRobin(int auctionId, int assetId)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         {
            var asset = await _assetsRepository.GetByIdAsync(assetId);
            if (asset == null) return;

            var highestBid = await _autoBidRepository.GetHighestBidAsync(assetId);

            var autoBids = await _autoBidRepository.GetActiveAutoBidsForAssetAsync(auctionId, assetId);

            if (!autoBids.Any()) return;

            var sortedAutoBids = autoBids.OrderBy(ab => ab.UpdatedDate).ToList();

            foreach (var autobid in sortedAutoBids)
            {
                if (!autobid.IsActive)
                    continue;

                if(highestBid != null && highestBid.UserId == autobid.UserId)
                {
                    continue;
                }
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var currentHighestBid = await _bidRepository.GetHighestBidAmountAsync(assetId);

                    if (currentHighestBid != (highestBid?.BidAmount ?? null))
                    {
                        await _unitOfWork.RollbackAsync();
                        return; 
                    }

                    // Calculate next bid amount after concurrency check
                    decimal nextBidAmount = (decimal)(currentHighestBid.HasValue
                        ? currentHighestBid.Value + asset.MinIncrement
                        : asset.StartingPrice);

                    if (autobid.MaxBidAmount < nextBidAmount)
                    {
                        await _unitOfWork.RollbackAsync();
                        continue; // Try next autobid in the round robin
                    }


                    // for giving notification to the user 
                    var previousWinningBid = await _bidRepository.GetWinningBidByAssetIdAsync(assetId);

                    if (previousWinningBid != null && previousWinningBid.UserId != autobid.UserId)
                    {
                        // Send outbid notification
                        var notification = new TblNotification
                        {
                            NotificationId = Guid.NewGuid(),
                            UserId = previousWinningBid.UserId,
                            Title = "You've been outbid",
                            Message = $"Your bid on asset '{asset.Title}' has been outbid by another user.",
                            CreatedAt = DateTime.UtcNow,
                            ExpiresAt = DateTime.UtcNow.AddDays(2),
                            AssetId = assetId,
                            AuctionId = auctionId,
                            IsRead = false
                        };

                        await _notificationRepository.CreateAsync(notification);

                        var notificationDto = new NotificationDto
                        {
                            UserId = notification.UserId,
                            Title = notification.Title,
                            Message = notification.Message,
                            ExpiresAt = notification.ExpiresAt,
                            AssetId = notification.AssetId,
                            AuctionId = notification.AuctionId,
                            IsRead = notification.IsRead
                        };

                        await _notificationBroadcaster.NotifyByUserId(notificationDto);
                    }


                    await _bidRepository.UnsetPreviousWinningBidAsync(assetId);

                    await _autoBidRepository.ExtendAuctionIfCloseToEndAsync(auctionId, DateTime.UtcNow);

                    var bid = new tblBid
                    {
                        AuctionId = auctionId,
                        AssetId = assetId,
                        UserId = autobid.UserId,
                        BidAmount = nextBidAmount,
                        BidTime = DateTime.UtcNow,
                        IsWinningBid = true,
                        IsAutoBid = true
                    };


                    await _bidRepository.AddBidAsync(bid);

                    autobid.UpdatedDate = DateTime.UtcNow;
                    await _autoBidRepository.UpdateAutoBidAsync(autobid);

                    await _unitOfWork.CommitAsync();

                    var bidCount = await _bidRepository.CountBidsByAssetIdAsync(assetId);
                    await _notificationService.NotifyNewBidAsync(auctionId, assetId, new
                    {
                        bidCount,
                        auctionId,
                        assetId,
                        autobid.UserId,
                        BidAmount = nextBidAmount,  
                        BidTime = DateTime.UtcNow
                    });

                    break; 
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }
        }
    }
}