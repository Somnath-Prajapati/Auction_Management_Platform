using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Reports
{
    public class DepositTransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }                  // New: From tblUsers

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }       // New: From tblTransactionTypes

        public int? PaymentMethodId { get; set; }
        public string? PaymentMethodName { get; set; }        // New: From tblPaymentMethods

        public int? CardTypeId { get; set; }
        public string? CardTypeName { get; set; }             // New: From tblCardTypes

        public string? MerchantTransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }                // New: From tblStatus

        public string? Notes { get; set; }
        public string? DocumentPath { get; set; }

        public long? CreatedByAdminID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class DepositTransactionResultDto
    {
        public List<DepositTransactionDto> Transactions { get; set; } = new();
        public int DepositTransactionCount { get; set; }
    }

}
