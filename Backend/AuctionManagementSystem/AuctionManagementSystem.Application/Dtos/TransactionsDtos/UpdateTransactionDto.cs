using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class UpdateTransactionDto : CreateTransactionDto
    {
        public int TransactionId { get; set; }
    }

}
