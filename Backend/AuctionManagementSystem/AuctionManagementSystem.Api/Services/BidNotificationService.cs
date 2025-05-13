using AuctionManagementSystem.Api.Hubs;
using AuctionManagementSystem.Application.Contracts.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace AuctionManagementSystem.Api.Services
{
    public class BidNotificationService : IBidNotificationService
    {
        private readonly IHubContext<BidHub> _hubContext;

        public BidNotificationService(IHubContext<BidHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNewBidAsync(int auctionId, int assetId, object bidData)
        {
            string group = $"auction-{auctionId}-asset-{assetId}";
            //await _hubContext.Clients.Group(group).SendAsync("ReceiveNewBid", bidData);
            await _hubContext.Clients.All.SendAsync("ReceiveNewBid", bidData);

        }
    }

}
