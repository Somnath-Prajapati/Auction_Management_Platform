using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Domain.Entities.Notification
{
    public class tblNotificationTranslation
    {
        public int NotificationTranslationId { get; set; }

        public Guid NotificationId { get; set; } 

        public int LanguageId { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }

       
        public TblNotification Notification { get; set; } = null!;
    }

}
