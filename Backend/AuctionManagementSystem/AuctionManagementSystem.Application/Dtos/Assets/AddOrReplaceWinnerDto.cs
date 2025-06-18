using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AddOrReplaceWinnerDto
    {
        public int AssetId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public decimal AwardedPrice { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public bool Approved { get; set; } = false;
    }

}
