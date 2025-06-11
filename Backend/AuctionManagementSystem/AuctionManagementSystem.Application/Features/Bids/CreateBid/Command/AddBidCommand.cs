using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.CreateBid.Command
{
    public record AddBidCommand(int AuctionId, int AssetId, int UserId, decimal BidAmount) : IRequest<int>;
}
