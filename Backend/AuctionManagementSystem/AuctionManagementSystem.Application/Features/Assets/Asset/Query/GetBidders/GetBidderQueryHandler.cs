using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetBidders
{
    public class GetBidderQueryHandler : IRequestHandler<GetBidderQuery, IEnumerable<TopBidderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAssetsRepository _assetsRepository;

        public GetBidderQueryHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopBidderDto>> Handle(GetBidderQuery request, CancellationToken cancellationToken)
        {
            var result = await _assetsRepository.GetAllBidders(request.assetId, request.auctionId);
            
            return _mapper.Map<IEnumerable<TopBidderDto>>(result);

        }
    }
}
