using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Query.SearchAsset
{
    public class SearchAssetQueryHandler : IRequestHandler<SearchAssetQuery, IEnumerable<GetAssetsDto>>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        public SearchAssetQueryHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAssetsDto>> Handle(SearchAssetQuery request, CancellationToken cancellationToken)
        {
           var asset = await _assetsRepository.SearchAsset(request.name);
        
            return _mapper.Map<IEnumerable<GetAssetsDto>>(asset);
        }


    }
}
