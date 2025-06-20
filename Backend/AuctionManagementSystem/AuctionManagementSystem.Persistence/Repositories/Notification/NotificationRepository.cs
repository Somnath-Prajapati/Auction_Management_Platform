using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AuctionManagementDbContext _context;

        public NotificationRepository(AuctionManagementDbContext context)
        {
            _context = context;
        } 

        public async Task CreateAsync(TblNotification notification)
        { 
            await _context.TblNotifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotififcationByUserId(int userId)
        {
            var notifications = await _context.TblNotifications
                .Where(n => n.UserId == userId && !n.IsDeleted)
                .ToListAsync();

            if (notifications.Any())
            {
                foreach (var notification in notifications)
                {
                    notification.IsDeleted = true;
                    notification.DeletedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }
        }



        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _context.TblNotifications.FindAsync(notificationId);
            if (notification != null && notification.UserId != null)
            {
                notification.IsRead = true;
                _context.TblNotifications.Update(notification);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddNotificationTranslationAsync(tblNotificationTranslation translation)
        {
            await _context.TblNotificationTranslations.AddAsync(translation);
            await _context.SaveChangesAsync();
        }


    }

}
