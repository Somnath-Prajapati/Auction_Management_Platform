using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Bids
{
    public class BidStatsBluckDto
    {
        public int AssetId { get; set; }
        public int BidCount { get; set; }
        public decimal HighestBid { get; set; }
    }
}
