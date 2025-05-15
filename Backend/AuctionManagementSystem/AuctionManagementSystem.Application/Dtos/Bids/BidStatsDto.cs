using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Bids
{
    public class BidStatsDto
    {
        public decimal HighestBid { get; set; }
        public int BidCount { get; set; }
    }
}
