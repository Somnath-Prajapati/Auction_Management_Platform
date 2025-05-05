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

namespace AuctionManagementSystem.Application.Features.Assets.AssetDetails.Query
{
    public class GetAssetDetailQueryHandler : IRequestHandler<GetAssetDetailQuery, IEnumerable<AssetDetailDto?>>
    {
        private readonly IAssetDetailRepository _assetDetailRepository;
        private readonly IMapper _mapper;
        public GetAssetDetailQueryHandler(IAssetDetailRepository assetDetailRepository, IMapper mapper)
        {
            _assetDetailRepository = assetDetailRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetDetailDto?>> Handle(GetAssetDetailQuery request, CancellationToken cancellationToken)
        {
            var assetdetails = await _assetDetailRepository.GetDetailsAsync();

            return _mapper.Map<IEnumerable<AssetDetailDto>>(assetdetails);
        }


    }

}
