using System.Linq.Expressions;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Contracts.RealTime;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Bids;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Domain.Models;
using AutoMapper;
using MediatR;


namespace AuctionManagementSystem.Application.Features.Bids.CreateBid.Command
{
    public class AddBidCommandHandler : IRequestHandler<AddBidCommand, int>
    {
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        private readonly IAuctionAssetRepository _auctionAssetRepository;
        private readonly IAssetsRepository _assetsRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidNotificationService _notificationService;
        private readonly IAutoBidRepository _autoBidRepo;
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;
        private readonly IUserDepositRepository _userDepositRepository;

        public AddBidCommandHandler(IBidRepository bidRepository, IMapper mapper, IAuctionAssetRepository auctionAssetRepository, IUnitOfWorkAuth unitOfWork, IAssetsRepository assetsRepository, IAuctionRepository auctionRepository, IBidNotificationService notificationService, IUserRepository userRepository, INotificationRepository notificationRepository, INotificationBroadcaster notificationBroadcaster , IAutoBidRepository autoBidRepo, IUserDepositRepository userDepositRepository)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
            _auctionAssetRepository = auctionAssetRepository;
            _unitOfWork = unitOfWork;
            _assetsRepository = assetsRepository;
            _auctionRepository = auctionRepository;
            _notificationService = notificationService;
            _autoBidRepo = autoBidRepo;
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
            _notificationBroadcaster = notificationBroadcaster;
            _userDepositRepository = userDepositRepository;
        }

        public async Task<int> Handle(AddBidCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {


                bool isValid = await _auctionAssetRepository.AssetExistsInAuctionAsync(request.AuctionId, request.AssetId);
                var user = await _userRepository.GetUserById(request.UserId);
                if(user == null)
                {
                    throw new BadRequestException("No User Available");
                }
                if (!isValid)
                    throw new BadRequestException("The asset does not belong to the specified auction.");

                var auction = await _auctionRepository.GetByIdAsync(request.AuctionId);
                if (auction == null || auction.EndDateTime < DateTime.UtcNow || auction.IsDeleted)
                {
                    throw new NotFoundException("Cannot place a bid. The auction has expired or is inactive.");
                }

                var asset = await _assetsRepository.GetByIdAsync(request.AssetId);
                if (asset == null)
                    throw new NotFoundException("Asset not found.");

                var isAutoBid = await _autoBidRepo.GetByUserAuctionAssetAsync(request.UserId, request.AuctionId, request.AssetId);
                if (isAutoBid != null && isAutoBid.IsActive)
                {
                    throw new NotFoundException("Auto-bid is currently active. Please disable it before placing a manual bid.");
                }

                var highestBid = await _bidRepository.GetHighestBidAmountAsync(request.AssetId);
                if (!highestBid.HasValue)
                {
                    if (request.BidAmount < asset.StartingPrice)
                        throw new BadRequestException($"First bid must be at least the starting price: {asset.StartingPrice}");
                }
                else
                {
                    var requiredMinBid = highestBid.Value + asset.MinIncrement;
                    if (request.BidAmount < requiredMinBid)
                        throw new BadRequestException($"Bid must be at least {requiredMinBid} (Min Increment: {asset.MinIncrement})");
                }
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
                    var uid = previousWinningBid.UserId;
                    var user1 = await _userRepository.GetUserById(uid);
                    var auditLog = new TblUserLimitAuditLog
                    {
                        UserId = user1.UserId,
                        ActionType = "Outbid Return",
                        OldDeposit = user1.Deposit, // Assuming this is available
                        NewDeposit = user1.Deposit, // No change here
                        OldTotalLimit = user1.TotalLimit, // No change here
                        NewTotalLimit = user1.TotalLimit,
                        OldAvailableLimit = user1.AvailableLimit,
                        NewAvailableLimit = user1.AvailableLimit+previousWinningBid.BidAmount,
                        Notes = $"Outbid for the {user1.UserId} on the asset ID {asset.AssetId}",
                        ChangedBy = user.UserId, // or system/admin ID if different
                        ChangedDate = DateTime.UtcNow
                    };

                    await _userDepositRepository.AddAsync(auditLog);
                    user1.AvailableLimit = user1.AvailableLimit + previousWinningBid.BidAmount;


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

                await _bidRepository.UnsetPreviousWinningBidAsync(request.AssetId);

                await _autoBidRepo.ExtendAuctionIfCloseToEndAsync(request.AuctionId, DateTime.UtcNow);

                var bid = _mapper.Map<tblBid>(request);
                bid.IsWinningBid = true;

                var bidId = await _bidRepository.AddBidAsync(bid);
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
                       request.UserId,
                       request.BidAmount,
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
