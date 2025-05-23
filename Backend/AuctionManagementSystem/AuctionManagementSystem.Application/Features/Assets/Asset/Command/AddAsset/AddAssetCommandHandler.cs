using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.AddAsset
{
    public class AddAssetCommandHandler : IRequestHandler<AddAssetCommand, CreateAssetsDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public AddAssetCommandHandler(
            IMapper mapper,
            IAssetsRepository assetsRepository,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<CreateAssetsDto> Handle(AddAssetCommand request, CancellationToken cancellationToken)
        {
            var assetEntity = _mapper.Map<TblAsset>(request.AssetsDto);
            assetEntity.CreatedAt = DateTime.UtcNow;
            //assetEntity.Cre = _currentUser.UserId.ToString();

            var createdAsset = await _assetsRepository.AddAsset(assetEntity);

            // Log audit trail for creation
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Asset",
                changeType: "New",
                recordId: createdAsset.AssetId,
                beforeChange: null,
                afterChange: createdAsset
            );

            Console.WriteLine($"Asset Created By: {_currentUser.Username} ({_currentUser.UserId}) with Role: {_currentUser.RoleName}");

            return _mapper.Map<CreateAssetsDto>(createdAsset);
        }
    }
}
