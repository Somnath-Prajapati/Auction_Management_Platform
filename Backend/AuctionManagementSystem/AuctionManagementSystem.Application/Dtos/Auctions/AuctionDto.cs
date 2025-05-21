using System;

namespace AuctionManagementSystem.Application.Dtos.Auctions
{
    public class AuctionDto
    {
        public int AuctionId { get; set; }
        public decimal TotalPrice { get; set; }
        public string AuctionNumber { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Type { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int StatusId { get; set; }

        public string? StatusName { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public int IncrementalTime { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        // Optional: Additional helper properties, if needed
        //public string? Duration => StartDateTime != null && EndDateTime != null ?
        //                            $"{(EndDateTime - StartDateTime).TotalHours} hours" : null;
    }
}
