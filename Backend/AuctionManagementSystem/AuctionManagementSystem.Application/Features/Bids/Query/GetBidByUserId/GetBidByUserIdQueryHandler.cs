using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Domain.Entities.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.Query.GetBidByUserId
{
    public class GetBidByUserIdQueryHandler : IRequestHandler<GetBidByUserIdQuery, IEnumerable<tblBid>>
    {
        private readonly IBidRepository _bidRepository;

        public GetBidByUserIdQueryHandler(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        public async Task<IEnumerable<tblBid>> Handle(GetBidByUserIdQuery request, CancellationToken cancellationToken)
        {
            var bids = await _bidRepository.GetBidsByUserIdAsync(request.UserId);

            return bids;
           
        }
    }
}
