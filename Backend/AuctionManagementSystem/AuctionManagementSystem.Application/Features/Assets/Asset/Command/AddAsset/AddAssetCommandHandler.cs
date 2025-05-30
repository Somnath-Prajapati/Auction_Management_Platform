using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Domain.Entities.Notification;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.AddAsset
{
    public class AddAssetCommandHandler : IRequestHandler<AddAssetCommand, CreateAssetsDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;


        public AddAssetCommandHandler(
            IMapper mapper,
            IAssetsRepository assetsRepository,
            IAuditTrailService auditTrailService,
            INotificationRepository notificationRepository,
             INotificationBroadcaster notificationBroadcaster,
            ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
            _notificationRepository = notificationRepository;
            _notificationBroadcaster = notificationBroadcaster;
        }

        public async Task<CreateAssetsDto> Handle(AddAssetCommand request, CancellationToken cancellationToken)
        {
            var assetEntity = _mapper.Map<TblAsset>(request.AssetsDto);
            assetEntity.CreatedAt = DateTime.UtcNow;
            // assetEntity.CreatedBy = _currentUser.UserId.ToString(); // Optional field

            var createdAsset = await _assetsRepository.AddAsset(assetEntity);
            Console.WriteLine($"Created Asset ID: {createdAsset.AssetId}");


           

            // Serialize for audit log
            var afterChangeJson = JsonConvert.SerializeObject(createdAsset, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            // Log audit trail for creation
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Asset",
                changeType: "New",
                recordId: createdAsset.AssetId,
                beforeChange: null,
                afterChange: afterChangeJson
            );

            Console.WriteLine($"Asset Created By: {_currentUser.Username} ({_currentUser.UserId}) with Role: {_currentUser.RoleName}");

            var notification = new TblNotification
            {
                Id = Guid.NewGuid(),
                UserId = null,
                Title = $"New Asset Created with id :: {createdAsset.AssetId}",
                Message = $"Asset '{createdAsset.Title}' has been added.",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                IsRead = false,
                AssetId = createdAsset.AssetId,
                AuctionId = null
            };

            await _notificationRepository.CreateAsync(notification);
            var notificationDto = new NotificationDto
            {
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                ExpiresAt = notification.ExpiresAt
            };

            await _notificationBroadcaster.BroadcastNotificationAsync(notificationDto);
            return _mapper.Map<CreateAssetsDto>(createdAsset);
        }
    }
}
