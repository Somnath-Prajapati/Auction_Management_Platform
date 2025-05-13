using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.Asset
{

    public class TblWishlistItem
    {
        public int WishlistItemId { get; set; }

        public int UserId { get; set; }

        public int AssetId { get; set; }

        public DateTime AddedAt { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeletedDate { get; set; }

        // Navigation properties
        public TblUser User { get; set; }

        public TblAsset Asset { get; set; }
    }

}
