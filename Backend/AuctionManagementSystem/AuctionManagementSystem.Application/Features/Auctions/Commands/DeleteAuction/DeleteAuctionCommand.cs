using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.DeleteAuction
{
    public class DeleteAuctionCommand : IRequest<bool>
    {
        public int AuctionId { get; set; }
    }
}
