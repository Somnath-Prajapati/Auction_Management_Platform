using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Dtos.Auctions;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Queries.GetAuctionsByIds
{
    public class GetAuctionsByIdsQueryHandler : IRequestHandler<GetAuctionsByIdsQuery, List<AuctionDto>>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public GetAuctionsByIdsQueryHandler(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }
        public async Task<List<AuctionDto>> Handle(GetAuctionsByIdsQuery request, CancellationToken cancellationToken)
        {
            var auctions = await _auctionRepository.GetAuctionsByIdsAsync(request.AuctionIds);
            return _mapper.Map<List<AuctionDto>>(auctions);
        }
    }
}
