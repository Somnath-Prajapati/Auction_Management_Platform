using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.CreateBid.Query.GetBidById
{
    public record GetBidStatsByAssetIdQuery(int assetId) : IRequest<BidStatsDto>;
}
