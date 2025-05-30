using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Notification;

namespace AuctionManagementSystem.Application.Contracts.Notification
{
    public interface INotificationBroadcaster
    {
        Task BroadcastNotificationAsync(NotificationDto notificationDto);
        Task NotifyByUserId(NotificationDto notification);
    }

}
