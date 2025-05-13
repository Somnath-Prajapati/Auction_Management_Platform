using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Bids
{
    public class BidDto
    {
       public int AuctionId { get; set; }
       public int AssetId {  get; set; }
       public int UserId { get; set; }
       public decimal BidAmount { get; set; }
    }
}
