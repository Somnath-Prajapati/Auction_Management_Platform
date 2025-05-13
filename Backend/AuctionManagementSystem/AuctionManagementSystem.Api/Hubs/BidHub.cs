using Microsoft.AspNetCore.SignalR;

namespace AuctionManagementSystem.Api.Hubs
{
    public class BidHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
    }
}
