using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Notification;

namespace AuctionManagementSystem.Application.Contracts.Notification
{
    public interface INotificationRepository
    {
        Task CreateAsync(TblNotification notification);
        Task MarkAsReadAsync(Guid notificationId);
        Task DeleteNotififcationByUserId(int userId);
    }

}
