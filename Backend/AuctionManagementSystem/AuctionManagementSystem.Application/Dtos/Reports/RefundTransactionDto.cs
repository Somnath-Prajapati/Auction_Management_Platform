using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Reports
{
    public class RefundTransactionDto
    {
        public int TransactionId { get; set; }           // int
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }                   // int
        public int TransactionTypeId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? CardTypeId { get; set; }
        public string? MerchantTransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int StatusId { get; set; }
        public string? Notes { get; set; }
        public string? DocumentPath { get; set; }
        public long? CreatedByAdminID { get; set; }       // bigint → long?
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }                // int?
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }                // int?
        public DateTime? UpdatedDate { get; set; }
        public int? DeletedBy { get; set; }                // int?
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class RefundTransactionResultDto
    {
        public List<RefundTransactionDto> Transactions { get; set; } = new();
        public int RefundRequestCount { get; set; }
    }

}
