using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Bids;
using Microsoft.Identity.Client;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Notification;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Command.CreateAutoBid
{
    public class AddAutoBidCommandHandler : IRequestHandler<AddAutoBidCommand, int>
    {
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        private readonly IAuctionAssetRepository _auctionAssetRepository;
        private readonly IAssetsRepository _assetsRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidNotificationService _notificationService;
        private readonly IAutoBidRepository _autoBidRepo;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;


        public AddAutoBidCommandHandler(IBidRepository bidRepository, IMapper mapper, IAuctionAssetRepository auctionAssetRepository, IUnitOfWorkAuth unitOfWork, IAssetsRepository assetsRepository, IAuctionRepository auctionRepository, IBidNotificationService notificationService, IAutoBidRepository autoBidRepo, INotificationRepository notificationRepository, INotificationBroadcaster notificationBroadcaster)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
            _auctionAssetRepository = auctionAssetRepository;
            _unitOfWork = unitOfWork;
            _assetsRepository = assetsRepository;
            _auctionRepository = auctionRepository;
            _notificationService = notificationService;
            _autoBidRepo = autoBidRepo;
            _notificationRepository = notificationRepository;
            _notificationBroadcaster = notificationBroadcaster;
        }

        public async Task<int> Handle(AddAutoBidCommand request, CancellationToken cancellationToken)
        {

            await _unitOfWork.BeginTransactionAsync();
            try
            {

                // 1 check it is exist or not 
                bool isValid = await _auctionAssetRepository.AssetExistsInAuctionAsync(
                     request.AuctionId, request.AssetId);

                if (!isValid)
                    throw new BadRequestException("The asset does not belong to the specified auction.");

                // 2 check auction exist or not 
                var auction = await _auctionRepository.GetByIdAsync(request.AuctionId);
                if (auction == null || auction.EndDateTime < DateTime.UtcNow || auction.IsDeleted)
                {
                    throw new NotFoundException("Cannot place a bid. The auction has expired or is inactive.");
                }

                // 3 check asset exist or not
                var asset = await _assetsRepository.GetByIdAsync(request.AssetId);
                if (asset == null)
                    throw new NotFoundException("Asset not found.");


                // 4 get the highest bid 
                var highestBidAmount = await _bidRepository.GetHighestBidAmountAsync(request.AssetId);

                if (!highestBidAmount.HasValue)
                {
                    if (request.MaxBidAmount < asset.StartingPrice)
                        throw new BadRequestException($"Max Auto Bid must be at least the starting price: {asset.StartingPrice}");
                }
                else
                {
                    var minAllowed = highestBidAmount.Value + asset.MinIncrement;
                    if (request.MaxBidAmount < minAllowed)
                        throw new BadRequestException($"Max Auto Bid must be at least {minAllowed} (min increment)");
                }

                // 5 get the all user fot get user
                //var existingAutoBid = await _autoBidRepo.GetByUserAuctionAssetAsync(request.UserId, request.AuctionId, request.AssetId);

                var existingAutoBid = await _autoBidRepo.GetUserAutoBidAsync(request.UserId, request.AuctionId, request.AssetId);

                if (existingAutoBid != null)
                {
                    existingAutoBid.MaxBidAmount = request.MaxBidAmount;
                    existingAutoBid.UpdatedDate = DateTime.UtcNow;
                    existingAutoBid.IsActive = true;
                    await _autoBidRepo.UpdateAutoBidAsync(existingAutoBid);
                }
                else
                {
                    var newAutoBid = new TblAutoBid
                    {
                        UserId = request.UserId,
                        AssetId = request.AssetId,
                        AuctionId = request.AuctionId,
                        MaxBidAmount = request.MaxBidAmount,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };


                    await _autoBidRepo.AddAutoBidAsync(newAutoBid);
                }



                decimal immediateBidAmount;
                if (!highestBidAmount.HasValue)
                {
                    immediateBidAmount = asset.StartingPrice;
                }
                else
                {
                    decimal nextBid = (decimal)(highestBidAmount.Value + asset.MinIncrement);

                    if( nextBid > request.MaxBidAmount)
                    {

                        var notification = new TblNotification
                        {
                            Id = Guid.NewGuid(),
                            UserId = request.UserId,
                            Title = "You've set limit been outbid",
                            Message = $"Your bid on asset '{asset.Title}' Limit has been Reached.",
                            CreatedAt = DateTime.UtcNow,
                            ExpiresAt = DateTime.UtcNow.AddDays(2),
                            AssetId = request.AssetId,
                            AuctionId = request.AuctionId,
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

                        throw new BadRequestException($"You have reached your max auto-bid limit: {request.MaxBidAmount}");
                    }
                    
                    else
                    {
                        immediateBidAmount = nextBid;
                    }
                }
                // for giving notification to the user 

                var previousWinningBid = await _bidRepository.GetWinningBidByAssetIdAsync(request.AssetId);
                if (previousWinningBid != null && previousWinningBid.UserId != request.UserId)
                {
                    // Send outbid notification
                    var notification = new TblNotification
                    {
                        Id = Guid.NewGuid(),
                        UserId = previousWinningBid.UserId,
                        Title = "You've been outbid",
                        Message = $"Your bid on asset '{asset.Title}' has been outbid by another user.",
                        CreatedAt = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddDays(2),
                        AssetId = request.AssetId,
                        AuctionId = request.AuctionId,
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



                if (immediateBidAmount < asset.StartingPrice)
                    immediateBidAmount = asset.StartingPrice;


                
                await _bidRepository.UnsetPreviousWinningBidAsync(request.AssetId);

                await _autoBidRepo.ExtendAuctionIfCloseToEndAsync(request.AuctionId, DateTime.UtcNow);

                var existingBid = await _bidRepository.GetUserBidAsync(request.UserId, request.AuctionId, request.AssetId);
                int bidId;
                if (existingBid != null)
                {
                    existingBid.BidAmount = immediateBidAmount;
                    existingBid.BidTime = DateTime.UtcNow;
                    existingBid.IsWinningBid = true;
                    existingBid.IsAutoBid = true;

                    await _bidRepository.UpdateBidAsync(existingBid);
                    bidId = existingBid.BidId;

                }
                else
                {
                    var bid = new tblBid


                    {
                        AuctionId = request.AuctionId,
                        AssetId = request.AssetId,
                        UserId = request.UserId,
                        BidAmount = immediateBidAmount,
                        BidTime = DateTime.UtcNow,
                        IsWinningBid = true,
                        IsAutoBid = true
                    };

                    bidId = await _bidRepository.AddBidAsync(bid);
                }


                await _unitOfWork.CommitAsync();


                var updatedBidCount = await _bidRepository.CountBidsByAssetIdAsync(request.AssetId);

                await _notificationService.NotifyNewBidAsync(
                   request.AuctionId,
                   request.AssetId,
                   new
                   {
                       bidCount = updatedBidCount,
                       auctionId = request.AuctionId,
                       assetId = request.AssetId,
                       UserId = request.UserId,
                       BidAmount = immediateBidAmount,
                       BidTime = DateTime.UtcNow
                   });

                return bidId;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

    }
}

