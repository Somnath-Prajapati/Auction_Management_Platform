using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Transactions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction
{
    

    public class ProcessAutoRefundCommand : IRequest<int> { }

    
}
