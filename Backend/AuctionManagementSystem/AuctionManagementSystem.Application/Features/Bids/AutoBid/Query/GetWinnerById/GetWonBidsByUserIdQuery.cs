using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Query.GetWinnerById
{
    public record GetWonBidsByUserIdQuery(int userid) : IRequest<IEnumerable<WonBidDto>>;
   
}
