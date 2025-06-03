using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class UserTransactionDto
    {
        public string RefNo { get; set; }
        public string Request { get; set; }
        public DateTime? DateTime { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // e.g., Deposit, Refund
        public string Method { get; set; } // e.g., Bank Transfer, Stripe, etc.
        public string Status { get; set; } // e.g., Pending, Completed
        public DateTime? ApprovedDateTime { get; set; }
        public string ApprovedBy { get; set; }
        public string Notes { get; set; }
    }

}
