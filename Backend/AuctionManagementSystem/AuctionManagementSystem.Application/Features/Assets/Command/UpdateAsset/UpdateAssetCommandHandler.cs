using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Command.UpdateAsset
{
    public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        public UpdateAssetCommandHandler(IAssetsRepository assetsRepository, IMapper mapper)

        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
           
            var Asset = await _assetsRepository.GetByIdAsync(request.id);
            Console.WriteLine(Asset);

            if (Asset == null) {

                throw new Exception($"Asset with ID {request.id} not found.");
            }

            Asset.Title = request.AssetsDto.Title;
            Asset.CategoryId = request.AssetsDto.CategoryId;
            Asset.Deposit = request.AssetsDto.Deposit;
            Asset.SellerId = request.AssetsDto.SellerId;
            Asset.Commission = request.AssetsDto.Commission;
            Asset.StartingPrice = request.AssetsDto.StartingPrice;
            Asset.ReserveAmount = request.AssetsDto.ReserveAmount;
            Asset.IncrementalTime = request.AssetsDto.IncrementalTime;
            Asset.MinIncrement = request.AssetsDto.MinIncrement;
            Asset.MakeOffer = request.AssetsDto.MakeOffer;
            Asset.Featured = request.AssetsDto.Featured;
            Asset.AwardingId = request.AssetsDto.AwardingId;
            Asset.StatusId = request.AssetsDto.StatusId;
            Asset.Vatid = request.AssetsDto.Vatid;
            Asset.Vatpercent = request.AssetsDto.Vatpercent;
            Asset.CourtCaseNumber = request.AssetsDto.CourtCaseNumber;
            Asset.RegistrationDeadline = request.AssetsDto.RegistrationDeadline;
            Asset.MapLatitude = request.AssetsDto.MapLatitude;
            Asset.MapLongitude = request.AssetsDto.MapLongitude;
            Asset.AdminFees = request.AssetsDto.AdminFees;
            Asset.AuctionFees = request.AssetsDto.AuctionFees;
            Asset.BuyerCommission = request.AssetsDto.BuyerCommission;
            Asset.WinnerId = request.AssetsDto.WinnerId;
            Asset.SalesNotes = request.AssetsDto.SalesNotes;
            Asset.Description = request.AssetsDto.Description;

            Asset.UpdatedAt = DateTime.UtcNow;
            
            Console.WriteLine(Asset);
            
            await _assetsRepository.UpdateAsync(Asset);
            
        }
    }
}
