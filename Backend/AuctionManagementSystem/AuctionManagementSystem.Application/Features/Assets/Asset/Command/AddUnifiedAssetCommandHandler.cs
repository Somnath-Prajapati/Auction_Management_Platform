
using System.Formats.Asn1;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
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

        public AddUnifiedAssetCommandHandler(IMapper mapper, IAssetsRepository assetsRepository)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
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

            return createdAsset;
        }

    }

}
