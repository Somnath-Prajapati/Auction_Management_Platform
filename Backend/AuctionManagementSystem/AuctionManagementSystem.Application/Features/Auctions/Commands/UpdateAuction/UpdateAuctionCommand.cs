using System;
using AuctionManagementSystem.Application.Dtos.Auctions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction
{
    public class UpdateAuctionCommand : AuctionBaseCommand, IRequest<bool>
    {
        public int AuctionId { get; set; }
       
    }

}
