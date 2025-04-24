using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions.Commands.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest<TransactionDto>
    {
        public UpdateTransactionDto Transaction { get; set; } = null!;
    }
}
