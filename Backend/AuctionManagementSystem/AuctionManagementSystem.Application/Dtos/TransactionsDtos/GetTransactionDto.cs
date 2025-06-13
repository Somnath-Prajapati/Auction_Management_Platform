using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.User;
using EventStore.ClientAPI.UserManagement;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class GetTransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int TransactionTypeId { get; set; }
        public int PaymentMethodId { get; set; }
        public int? CardTypeId { get; set; }
        public string? MerchantTransactionId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int StatusId { get; set; }
        public string? Notes { get; set; }
        //public string? DocumentPath { get; set; }
        public string? DocumentUrl { get; set; }

        public long? CreatedByAdminId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string PaymentMethodName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public string TransactionTypeName { get; set; } = null!;
        public string? CardTypeName { get; set; }

        public string UserFullName { get; set; }
        //public virtual TblUser User { get; set; } = null!;
    }

}
