using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.Command.CreateBid
{
    public record AddBidCommand(int AuctionId,int AssetId,int UserId,decimal BidAmount) : IRequest<int>;
}
