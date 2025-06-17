
using System.Formats.Asn1;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Domain.Entities.Translations;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command
{
    public class AddUnifiedAssetCommandHandler : IRequestHandler<AddUnifiedAssetCommand, int>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IAuditTrailService _auditTrailService;
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;

        public AddUnifiedAssetCommandHandler(IMapper mapper, IAssetsRepository assetsRepository,INotificationRepository notificationRepository,
             INotificationBroadcaster notificationBroadcaster,
            IAuditTrailService auditTrailService, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
            _notificationBroadcaster = notificationBroadcaster;
            _notificationRepository = notificationRepository;
        }


        public async Task<int> Handle(AddUnifiedAssetCommand request, CancellationToken cancellationToken)
        {
            var dto = request.dto;
            var assetEntity = _mapper.Map<TblAsset>(dto);

            assetEntity.CreatedAt = DateTime.UtcNow;
            assetEntity.IsDeleted = true;

            if (dto.AuctionIds != null && dto.AuctionIds.Any())
            {
                var selectedAuctionId = dto.AuctionIds.First();
                bool isDirectAndActive = await _assetsRepository.HasAnyDirectAndActiveAuctionAsync(selectedAuctionId);
                assetEntity.IsAvailableForDirectSale = isDirectAndActive;
            }
            else
            {
                assetEntity.IsAvailableForDirectSale = false;
            }

            var createdAsset = await _assetsRepository.AddAssetForGallery(assetEntity);
            if(dto.LanguageId.HasValue && dto.LanguageId != 0)
            {
            var assetTranslation = new tblAssetTranslation
            {
                AssetId = createdAsset.AssetId,
                LanguageId = dto.LanguageId.Value,
                Title = dto.TranslatedTitle,
                Description = dto.TranslatedDescription,
                SalesNotes = dto.TranslatedSalesNotes,
                CreatedAt = DateTime.UtcNow
            };
            await _assetsRepository.AddAssetTranslationAsync(assetTranslation);

            }


            var notification = new TblNotification
            {
                NotificationId = Guid.NewGuid(),
                UserId = null,
                Title = $"New Asset Created with id :: {createdAsset}",
                Message = $"Asset '{dto.Title}' has been added.",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                IsRead = null,
                 AssetId = createdAsset.AssetId,
                AuctionId = null
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

            // AUDIT TRAIL LOGGING
            try
            {
                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "Asset",
                    changeType: "New",
                    recordId: createdAsset.AssetId,
                    beforeChange: null,
                    afterChange: createdAsset // 👈 No need to serialize manually unless your method requires it
                );

                Console.WriteLine($"Asset Created By: {_currentUser.Username} ({_currentUser.UserId}) with Role: {_currentUser.RoleName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audit log failed: {ex.Message}");
            }

            return createdAsset.AssetId;
        }

    }

}
