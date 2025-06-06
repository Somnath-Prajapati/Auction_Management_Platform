using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Reports
{
    public class StatementTransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class StatementAccountResultDto
    {
        public List<StatementTransactionDto> Transactions { get; set; }
        public int TotalTransactions { get; set; }
    }


}
