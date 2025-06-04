using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.Notification;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction
{
    public class CreateAuctionHandler : IRequestHandler<CreateAuctionCommand, int>
    {
        private readonly IAuctionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuctionJobScheduler _jobScheduler;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;

        public CreateAuctionHandler(
            IAuctionUnitOfWork unitOfWork,
            IMapper mapper,
            IAuctionJobScheduler jobScheduler,
            IAuditTrailService auditTrailService,
            INotificationRepository notificationRepository,
             INotificationBroadcaster notificationBroadcaster,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobScheduler = jobScheduler;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
            _notificationRepository = notificationRepository;
            _notificationBroadcaster = notificationBroadcaster;
        }

        public async Task<int> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = _mapper.Map<TblAuction>(request);
            auction.CreatedBy = _currentUser.UserId.ToString();

            if (auction.Type == "Auction")
            {
                var newJobId = _jobScheduler.ScheduleAuctionClosing(auction.AuctionId, auction.EndDateTime);
                auction.HangfireJobId = newJobId;
            }


            await _unitOfWork.AuctionRepository.AddAsync(auction);
            await _unitOfWork.SaveAsync();


            // Log the creation in the audit trail
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Auction",
                changeType: "New",
                recordId: auction.AuctionId,
                beforeChange: null,
                afterChange: auction
            );

            Console.WriteLine($"Auction Created By: {_currentUser.Username} ({_currentUser.UserId}) with Role: {_currentUser.RoleName}");

            var notification = new TblNotification
            {
                Id = Guid.NewGuid(),
                UserId = null,
                Title = $"New Auction Created with id :: {auction.AuctionId}",
                Message = $"Auction '{auction.Title}' has been added.",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                IsRead = null,
                 AssetId = null,
                AuctionId = auction.AuctionId
            };

            await _notificationRepository.CreateAsync(notification);
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

            return auction.AuctionId;
        }
    }
}
