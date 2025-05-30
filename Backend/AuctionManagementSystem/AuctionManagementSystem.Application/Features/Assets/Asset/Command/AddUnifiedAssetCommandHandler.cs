
using System.Formats.Asn1;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Notification;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command
{
    public class AddUnifiedAssetCommandHandler : IRequestHandler<AddUnifiedAssetCommand, int>
    {

        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;


        public AddUnifiedAssetCommandHandler(IMapper mapper, IAssetsRepository assetsRepository, INotificationRepository notificationRepository,
             INotificationBroadcaster notificationBroadcaster)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
            _notificationRepository = notificationRepository;
            _notificationBroadcaster = notificationBroadcaster;
        }


        public async Task<int> Handle(AddUnifiedAssetCommand request, CancellationToken cancellationToken)
        {
            //Console.WriteLine("Details JSON: " + request.dto.DetailsJson); // Logs the details JSON

            var dto = request.dto;
            var assetEntity = _mapper.Map<TblAsset>(dto);

            assetEntity.CreatedAt = DateTime.UtcNow;

            assetEntity.IsDeleted = true;

            if (dto.AuctionIds != null && dto.AuctionIds.Any())
            {
                var selectedAuctionId = dto.AuctionIds.First(); 
                bool isDirectAndActive = await _assetsRepository.HasAnyDirectAndActiveAuctionAsync(selectedAuctionId);
                if(isDirectAndActive == true)
                {
                assetEntity.IsAvailableForDirectSale = true;

                }
                else
                {
                    assetEntity.IsAvailableForDirectSale = false;
                }
            }
            else
            {
                assetEntity.IsAvailableForDirectSale = false;
            }

           
            var createdAsset = await _assetsRepository.AddAssetForGallery(assetEntity);
            var notification = new TblNotification
            {
                Id = Guid.NewGuid(),
                UserId = null,
                Title = $"New Asset Created with id :: {createdAsset}",
                Message = $"Asset '{dto.Title}' has been added.",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                IsRead = null,
                 AssetId = createdAsset,
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

            return createdAsset;
        }

    }

}
