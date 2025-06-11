using AuctionManagementSystem.Application.Dtos.Auctions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction
{
    public class CreateAuctionCommand : AuctionBaseCommand, IRequest<AuctionDto>
    {
        // Additional create-specific fields can go here
        public string? UserId { get; set; }
        public string? AuctionNumber { get; set; }
    }

}

