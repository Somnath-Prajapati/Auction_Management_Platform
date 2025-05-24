using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Bids;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Command.CreateAutoBid
{
    public record AddAutoBidCommand(int UserId ,int AuctionId,int AssetId, decimal MaxBidAmount) : IRequest<int>;
}
