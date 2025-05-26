using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.UpdateAsset
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public UpdateAssetCommandHandler(
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

        public async Task Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _assetsRepository.GetIdDeleteAsync(request.id);

            if (asset == null)
            {
                throw new Exception($"Asset with ID {request.id} not found.");
            }


            // Update fields
            asset.Title = request.AssetsDto.Title;
            asset.CategoryId = request.AssetsDto.CategoryId;
            asset.Deposit = request.AssetsDto.Deposit;
            asset.SellerId = request.AssetsDto.SellerId;
            asset.Commission = request.AssetsDto.Commission;
            asset.StartingPrice = request.AssetsDto.StartingPrice;
            asset.ReserveAmount = request.AssetsDto.ReserveAmount;
            asset.IncrementalTime = request.AssetsDto.IncrementalTime;
            asset.MinIncrement = request.AssetsDto.MinIncrement;
            asset.MakeOffer = request.AssetsDto.MakeOffer;
            asset.Featured = request.AssetsDto.Featured;
            asset.AwardingId = request.AssetsDto.AwardingId;
            asset.StatusId = request.AssetsDto.StatusId;
            asset.Vatid = request.AssetsDto.Vatid;
            asset.Vatpercent = request.AssetsDto.Vatpercent;
            asset.CourtCaseNumber = request.AssetsDto.CourtCaseNumber;
            asset.RegistrationDeadline = request.AssetsDto.RegistrationDeadline;
            asset.MapLatitude = request.AssetsDto.MapLatitude;
            asset.MapLongitude = request.AssetsDto.MapLongitude;
            asset.AdminFees = request.AssetsDto.AdminFees;
            asset.AuctionFees = request.AssetsDto.AuctionFees;
            asset.BuyerCommission = request.AssetsDto.BuyerCommission;
            asset.WinnerId = request.AssetsDto.WinnerId;
            asset.SalesNotes = request.AssetsDto.SalesNotes;
            asset.Description = request.AssetsDto.Description;
            asset.UpdatedAt = DateTime.UtcNow;

            // Update in DB
            await _assetsRepository.UpdateAsync(asset);

            // Log to audit trail
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Asset",
                changeType: "Update",
                recordId: asset.AssetId,
                beforeChange: JsonConvert.SerializeObject(asset, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
    afterChange: JsonConvert.SerializeObject(asset, new JsonSerializerSettings
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    })
                );

            Console.WriteLine($"Asset Updated By: {_currentUser.Username} ({_currentUser.UserId})");
        }
    }
}
