using System;

namespace AuctionManagementSystem.Application.Dtos
{
    public class FaqDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 