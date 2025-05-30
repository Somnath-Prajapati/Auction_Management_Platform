
using System;
using System.Collections.Generic;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.Bids
{
    public partial class TblAutoBid
    {
        public int AutoBidId { get; set; }

        public int UserId { get; set; }

        public int AuctionId { get; set; }

        public int AssetId { get; set; }

        public decimal MaxBidAmount { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual TblAsset Asset { get; set; }

        public virtual TblAuction Auction { get; set; }

        public virtual TblUser User { get; set; }
    }
}