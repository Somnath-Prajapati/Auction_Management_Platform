using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionManagementSystem.Domain.Entities
{
    [Table("chatbotfaqs")]
    public class Faq
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; } // e.g., Auction, Asset, Payment, etc.
        public string Tags { get; set; } // Comma-separated tags for search/filter
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
} 