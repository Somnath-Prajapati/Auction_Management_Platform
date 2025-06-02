using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace AuctionManagementSystem.Api.Hubs
{
    public class BidHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");

            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(role))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, role);
                Console.WriteLine($"Added {Context.ConnectionId} to group {role}");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(role))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
                Console.WriteLine($"Removed {Context.ConnectionId} from group {role}");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
