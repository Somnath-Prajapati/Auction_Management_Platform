using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.GetFeaturedAssets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.GetFeaturedAssets.Query
{
    public class GetFeaturedAssetsQueryHandler : IRequestHandler<GetFeaturedAssetsQuery, List<FeaturedAssetDto>>
    {
        private readonly IAssetsRepository _repo;
        private readonly IMapper _mapper;

        public GetFeaturedAssetsQueryHandler(IAssetsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<FeaturedAssetDto>> Handle(GetFeaturedAssetsQuery request, CancellationToken cancellationToken)
        {
            var assets = await _repo.GetFeaturedAssetsAsync();
            return _mapper.Map<List<FeaturedAssetDto>>(assets);
        }
    }

}
