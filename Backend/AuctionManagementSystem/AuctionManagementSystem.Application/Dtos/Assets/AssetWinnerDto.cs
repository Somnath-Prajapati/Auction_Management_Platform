using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetWinnerDto
    {
        public int winnerId {  get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public decimal? AwardedPrice { get; set; }

        public string Reason { get; set; }

        public string Note { get; set; }

        public bool? Approved { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}
