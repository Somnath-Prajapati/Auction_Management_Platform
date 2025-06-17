using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.Notification
{
    public class TblNotification
    {
        [Key]
        public Guid NotificationId { get; set; }  
        public int? UserId { get; set; }
        public int? AssetId { get; set; }
        public int? AuctionId { get; set; }
        public string Title { get; set; }  
        public string Message { get; set; }
        public bool? IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } 
        public DateTime? ExpiresAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual TblUser? User { get; set; }

        [ForeignKey(nameof(AssetId))]
        public virtual TblAsset? Asset { get; set; }

        [ForeignKey(nameof(AuctionId))]
        public virtual TblAuction? Auction { get; set; }
    }
}
