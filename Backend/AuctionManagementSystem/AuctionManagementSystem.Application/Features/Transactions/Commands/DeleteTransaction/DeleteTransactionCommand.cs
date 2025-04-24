using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions.Commands.DeleteTransaction
{
    public class DeleteTransactionCommand : IRequest<bool>
    {
        public int TransactionId { get; set; }
    }
}
