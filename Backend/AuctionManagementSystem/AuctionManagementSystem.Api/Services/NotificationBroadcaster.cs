using AuctionManagementSystem.Api.Hubs;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Dtos.Notification;
using Microsoft.AspNetCore.SignalR;

namespace AuctionManagementSystem.Api.Services
{
    public class NotificationBroadcaster: INotificationBroadcaster
    {
        private readonly IHubContext<BidHub> _hubContext;
        public NotificationBroadcaster(IHubContext<BidHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task BroadcastNotificationAsync(NotificationDto notification)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }
        public async Task NotifyByUserId(NotificationDto notification)
        {
            await _hubContext.Clients.User(notification.UserId.ToString()).SendAsync("ReceiveNotification", notification);
    
        }
        //public async Task NotifyByRole(NotificationDto notification)
        //{
        //    await _hubContext.Clients.User()
        //}
    }
}
