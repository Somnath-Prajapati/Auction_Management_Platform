using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.DeleteAsset
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public DeleteAssetCommandHandler(
            IAssetsRepository assetsRepository,
            IMapper mapper,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            if (request.id == null) throw new Exception("Asset ID cannot be null.");

            var asset = await _assetsRepository.GetIdDeleteAsync(request.id);

            if (asset == null)
                throw new Exception($"Asset with ID {request.id} not found.");

            // Audit Trail Before Deletion
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Asset",
                changeType: "Delete",
                recordId: asset.AssetId,
                beforeChange: asset,
                afterChange: null
            );

            await _assetsRepository.DeleteAsync(asset);

            Console.WriteLine($"Asset Deleted by {_currentUser.Username} (ID: {_currentUser.UserId}) with Role: {_currentUser.RoleName}");
        }
    }
}
