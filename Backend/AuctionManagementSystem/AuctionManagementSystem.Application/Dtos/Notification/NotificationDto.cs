using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Notification
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public int? UserId { get; set; } 
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool? IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int? AuctionId { get; set; }
        public int? AssetId { get; set; }

    }

}
