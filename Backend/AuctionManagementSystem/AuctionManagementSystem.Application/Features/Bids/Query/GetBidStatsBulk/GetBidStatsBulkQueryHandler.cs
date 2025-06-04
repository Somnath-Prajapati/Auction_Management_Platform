using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Dtos.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.Query.GetBidStatsBulk
{
    public class GetBidStatsBulkQueryHandler : IRequestHandler<GetBidStatsBulkQuery, List<BidStatsBluckDto>>
    {
        private readonly IBidRepository _bidRepository;

        public GetBidStatsBulkQueryHandler(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task<List<BidStatsBluckDto>> Handle(GetBidStatsBulkQuery request, CancellationToken cancellationToken)
        {
            return await _bidRepository.GetBidStatsByAssetIdsAsync(request.AssetIds);
        }
    }
}
