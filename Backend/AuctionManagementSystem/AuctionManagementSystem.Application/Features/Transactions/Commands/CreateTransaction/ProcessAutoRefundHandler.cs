using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Transactions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions.Commands.CreateTransaction
{
    public class ProcessAutoRefundHandler : IRequestHandler<ProcessAutoRefundCommand, int>
    {
        private readonly IUserDepositRepository _repository;
        private readonly TimeSpan _idleTimeLimit = TimeSpan.FromDays(1); // example 30 days

        public ProcessAutoRefundHandler(IUserDepositRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(ProcessAutoRefundCommand request, CancellationToken cancellationToken)
        {
            var depositsToRefund = await _repository.GetDepositsToRefundAsync(_idleTimeLimit);
            int refundsCreated = 0;

            foreach (var deposit in depositsToRefund)
            {
                await _repository.AddRefundTransactionAsync(deposit.UserId, deposit.DepositAmount);

                // Optionally mark deposit ModifiedAt to now to avoid repeated refund requests
                await _repository.UpdateUserDepositModifiedAtAsync(deposit.Id, DateTime.UtcNow);

                refundsCreated++;
            }

            return refundsCreated;
        }
    }
}
