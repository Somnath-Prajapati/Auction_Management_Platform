using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class FilteredTransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string TransactionTypeName { get; set; }
        public string PaymentMethodName { get; set; }
        public string CardTypeName { get; set; }
        public string UserName { get; set; }
        // Add any additional properties returned by the SP
    }

    //public class StatementTransactionDto
    //{
    //    public int TransactionId { get; set; }
    //    public string TransactionNumber { get; set; }
    //    public decimal Amount { get; set; }
    //    public DateTime TransactionDateTime { get; set; }

    //    public int StatusId { get; set; }
    //    public string StatusName { get; set; }

    //    public int? TransactionTypeId { get; set; }
    //    public string TransactionTypeName { get; set; }

    //    public int? PaymentMethodId { get; set; }
    //    public string PaymentMethodName { get; set; }

    //    public int? CardTypeId { get; set; }
    //    public string CardTypeName { get; set; }

    //    public int UserId { get; set; }
    //    public string UserName { get; set; }
    //}


    public class FilteredTransactionResultDto
    {
        public int TotalCount { get; set; }
        public List<FilteredTransactionDto> Transactions { get; set; }
    }

}
