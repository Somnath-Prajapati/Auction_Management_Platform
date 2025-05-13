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
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public string CardType { get; set; }
        public string MerchantTransactionId { get; set; }
        public string Notes { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string TransactionType { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string DocumentUrl { get; set; }

    }

}
