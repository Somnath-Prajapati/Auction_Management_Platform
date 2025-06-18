using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class TopBidderRaw
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
        public bool IsAutoBid { get; set; }
    }

}
