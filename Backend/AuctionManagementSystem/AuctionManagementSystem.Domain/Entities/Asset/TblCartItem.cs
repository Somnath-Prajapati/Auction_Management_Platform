using System;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities
{
    public class TblCartItem
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedDate { get; set; }

        // Optional: Navigation properties
        public TblAsset Asset { get; set; }
        public TblUser User { get; set; }
    }
}
