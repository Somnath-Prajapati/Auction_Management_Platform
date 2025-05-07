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

namespace AuctionManagementSystem.Application.Features.Assets.AssetDetails.Query.GetDetailsById
{
    public class GetAssetDetailByIdQueryHandler : IRequestHandler<GetAssetDetailByIdQuery, GetAssetDetailsDto?>
    {
        private readonly IAssetDetailRepository _assetDetailRepository;
        private readonly IMapper _mapper;
        public GetAssetDetailByIdQueryHandler(IAssetDetailRepository assetDetailRepository, IMapper mapper)
        {
            _assetDetailRepository = assetDetailRepository;
            _mapper = mapper;
        }

        public async Task<GetAssetDetailsDto?> Handle(GetAssetDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var asset = await _assetDetailRepository.GetDetailsByIdAsync(request.Id);

            return _mapper.Map<GetAssetDetailsDto>(asset);
        }



    
    }

}
