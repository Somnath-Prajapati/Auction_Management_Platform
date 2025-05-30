using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<TransactionDto>
    {
        public CreateTransactionDto Transaction { get; set; }
    }




   


}
