using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Bids
{
    public class AddAutoBidDto
    {
        public int UserId { get; set; }
        public int AuctionId { get; set; }
        public int AssetId { get; set; }
        public decimal MaxBidAmount { get; set; }
    }

}
