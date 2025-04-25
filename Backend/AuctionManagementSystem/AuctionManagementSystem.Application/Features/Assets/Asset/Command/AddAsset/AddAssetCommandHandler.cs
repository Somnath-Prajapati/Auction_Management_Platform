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

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Command.AddAsset
{
    public class AddAssetCommandHandler : IRequestHandler<AddAssetCommand, CreateAssetsDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        public AddAssetCommandHandler(IMapper mapper, IAssetsRepository assetsRepository)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
        }

        public async Task<CreateAssetsDto> Handle(AddAssetCommand request, CancellationToken cancellationToken)
        {
            var assetEntity = _mapper.Map<TblAsset>(request.AssetsDto);
            assetEntity.CreatedAt = DateTime.UtcNow;

            var createdAsset = await _assetsRepository.AddAsset(assetEntity);

            return _mapper.Map<CreateAssetsDto>(createdAsset);
        }




        //public async Task<CreateAssetDto> Handle(AddAssetCommand request, CancellationToken cancellationToken)
        //{
        //var assetEntity = _mapper.Map<TblAsset>(request.CreateAssetDto);
        //assetEntity.CreatedAt = DateTime.UtcNow;

        //    var createdAsset = await _assetsRepository.AddAsset(assetEntity);

        //    return _mapper.Map<GetAssetsDto>(createdAsset);
        //}
    }

}
