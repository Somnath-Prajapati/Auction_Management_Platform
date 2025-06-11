using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Dtos.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.Query.GetBidById
{
    
     public class GetBidStatsByAssetIdQueryHandler : IRequestHandler<GetBidStatsByAssetIdQuery, BidStatsDto>
    {
        private readonly IBidRepository _bidRepository;

        public GetBidStatsByAssetIdQueryHandler(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task<BidStatsDto> Handle(GetBidStatsByAssetIdQuery request, CancellationToken cancellationToken)
        {
            var (highestBid, bidCount) = await _bidRepository.GetBidStatsByAssetIdAsync(request.assetId);

            return new BidStatsDto
            {
                HighestBid = highestBid,
                BidCount = bidCount
            };
        }
    }
}
