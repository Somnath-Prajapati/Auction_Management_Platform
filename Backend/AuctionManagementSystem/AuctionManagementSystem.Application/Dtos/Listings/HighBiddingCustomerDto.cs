using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Listings
{
    public class HighBiddingCustomerDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int TotalManualBids { get; set; }
        public decimal TotalManualBidAmount { get; set; }
        public int TotalAutoBids { get; set; }
        public decimal TotalAutoBidAmount { get; set; }
        public int TotalBidsCount { get; set; }
        public decimal TotalBidAmount { get; set; }
    }

}
