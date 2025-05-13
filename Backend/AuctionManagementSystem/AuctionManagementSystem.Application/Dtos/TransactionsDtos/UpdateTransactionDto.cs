using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class UpdateTransactionDto
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int TransactionTypeId { get; set; }
        public int PaymentMethodId { get; set; }
        public int CardTypeId { get; set; }
        public string MerchantTransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int StatusId { get; set; }
        public string Notes { get; set; }
        public string DocumentPath { get; set; }  // If required
    }


}
