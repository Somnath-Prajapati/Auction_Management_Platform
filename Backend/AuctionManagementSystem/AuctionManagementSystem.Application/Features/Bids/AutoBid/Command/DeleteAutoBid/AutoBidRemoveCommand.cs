using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Command.DeleteAutoBid
{
    public record AutoBidRemoveCommand(int userId, int auctionId, int assetId) : IRequest;
}
