using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Domain.model;
using AuctionManagementSystem.Domain.Models;

namespace AuctionManagementSystem.Application.Contracts.Transactions
{
    public interface IUserDepositRepository
    {
        Task<List<TblUserDeposit>> GetDepositsToRefundAsync(TimeSpan idleTimeLimit);
        Task AddRefundTransactionAsync(int userId, decimal amount);
        Task UpdateUserDepositModifiedAtAsync(int depositId, DateTime modifiedAt);

        Task AddAsync(TblUserDeposit userDeposit);

        Task<UserDepositLimitDto?> GetUserDepositLimitsAsync(int userId);

        Task AddAsync(TblUserLimitAuditLog log);

    }

}
