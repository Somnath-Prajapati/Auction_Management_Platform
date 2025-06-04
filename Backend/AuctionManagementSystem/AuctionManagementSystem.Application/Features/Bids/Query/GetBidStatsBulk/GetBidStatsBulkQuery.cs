using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.Query.GetBidStatsBulk
{
    public record GetBidStatsBulkQuery(List<int> AssetIds) : IRequest<List<BidStatsBluckDto>>;
}
