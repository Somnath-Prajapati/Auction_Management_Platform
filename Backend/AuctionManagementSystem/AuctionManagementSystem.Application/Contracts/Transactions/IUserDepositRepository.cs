using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.model;

namespace AuctionManagementSystem.Application.Contracts.Transactions
{
    public interface IUserDepositRepository
    {
        Task<List<TblUserDeposit>> GetDepositsToRefundAsync(TimeSpan idleTimeLimit);
        Task AddRefundTransactionAsync(int userId, decimal amount);
        Task UpdateUserDepositModifiedAtAsync(int depositId, DateTime modifiedAt);
    }

}
