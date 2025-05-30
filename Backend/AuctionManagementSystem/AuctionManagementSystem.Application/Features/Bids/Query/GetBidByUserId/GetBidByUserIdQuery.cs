using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;
using AuctionManagementSystem.Domain.Entities.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.Query.GetBidByUserId
{
    public record GetBidByUserIdQuery(int UserId) : IRequest<IEnumerable<tblBid>>;
}
