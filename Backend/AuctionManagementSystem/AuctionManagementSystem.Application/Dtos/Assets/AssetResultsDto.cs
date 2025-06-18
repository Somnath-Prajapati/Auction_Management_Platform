using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetResultsDto
    {
        public int TotalBids { get; set; }
        public int TotalBidders { get; set; }
        public decimal StartPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal CommissionPercentage { get; set; }
        public decimal TotalPayable { get; set; }
    }

}
