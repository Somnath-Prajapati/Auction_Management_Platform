using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public string UserFullName { get; set; } = null!;
        public string TransactionType { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string? CardType { get; set; }
        public string? MerchantTransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string Status { get; set; } = null!;
        public string? Notes { get; set; }
        public List<string> DocumentUrls { get; set; } = new();
    }

}
