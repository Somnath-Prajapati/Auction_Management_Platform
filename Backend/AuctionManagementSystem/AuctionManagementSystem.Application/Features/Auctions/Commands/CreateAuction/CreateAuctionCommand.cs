using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Auctions;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction
{
    public class CreateAuctionCommand : AuctionBaseCommand, IRequest<int>
    {
        // Additional create-specific fields can go here
    }

}

