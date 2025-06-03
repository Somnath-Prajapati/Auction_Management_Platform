using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Application.Features.Listings.Cart;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Query.GetWinnerById
{
    public class GetWonBidsByUserIdHandler : IRequestHandler<GetWonBidsByUserIdQuery, IEnumerable<WonBidDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        public GetWonBidsByUserIdHandler(IMapper mapper, IBidRepository bidRepository)
        {
            _mapper = mapper;
            _bidRepository = bidRepository;
        }

        public async Task<IEnumerable<WonBidDto>> Handle(GetWonBidsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var dto = await _bidRepository.GetWonBidsByUserIdAsync(request.userid);

            return _mapper.Map<IEnumerable<WonBidDto>>(dto);
        }
    }
}
