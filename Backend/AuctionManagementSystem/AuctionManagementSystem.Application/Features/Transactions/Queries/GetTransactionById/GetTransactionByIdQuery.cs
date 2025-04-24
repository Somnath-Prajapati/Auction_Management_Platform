using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions.Queries.GetTransactionById
{
    public class GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public int TransactionId { get; set; }
    }
}
