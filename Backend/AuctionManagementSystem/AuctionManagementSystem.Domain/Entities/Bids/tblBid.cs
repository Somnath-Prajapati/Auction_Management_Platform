using System;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.Bids
{
    public class tblBid
    {
        public int BidId { get; set; }

        public int AuctionId { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }

        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
        public bool IsWinningBid { get; set; }
        public DateTime CreatedDate { get; set; }

        public TblAuction Auction { get; set; }
        public TblAsset Asset { get; set; }
        public TblUser User { get; set; }
    }
}
