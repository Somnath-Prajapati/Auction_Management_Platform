using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Auctions
{
    public abstract class AuctionBaseCommand
    {
        public string AuctionNumber { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Type { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int StatusId { get; set; }
        public int IncrementalTime { get; set; }
        public int? CategoryId { get; set; }
    }

}
