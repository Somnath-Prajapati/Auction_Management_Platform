using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Domain.model;
using AuctionManagementSystem.Domain.Models;
using AuctionManagementSystem.Infrastructure.Persistence.Repositories;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Transactions
{
    public class UserDepositRepository : IUserDepositRepository
    {
        private readonly AuctionManagementDbContext _context;
        private readonly ITransactionRepository _transactionRepository;

        public UserDepositRepository(AuctionManagementDbContext context, ITransactionRepository transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TblUserDeposit>> GetDepositsToRefundAsync(TimeSpan idleTimeLimit)
        {
            var cutoffDate = DateTime.UtcNow.Subtract(idleTimeLimit);
            return await _context.TblUserDeposits
                .Where(d => !d.IsDeleted && d.DepositAmount > 0 && d.ModifiedAt <= cutoffDate)
                .ToListAsync();
        }

        public async Task AddRefundTransactionAsync(int userId, decimal amount) // Updated return type to match interface
        {
            Console.WriteLine("refund transaction to be created");
            var refundTransaction = new TblTransaction
            {
                UserId = userId,
                Amount = amount,
                TransactionTypeId = 3, // Refund
                StatusId = 1, // Pending approval
                CreatedAt = DateTime.UtcNow,
                PaymentMethodId = 2,
                UpdatedAt = null,
                DeletedDate = null,
                TransactionDateTime = DateTime.UtcNow,
                Notes = "System Refund Request Generated Account isn't active for the Set Period" // Fixed syntax issues
            };

            await _transactionRepository.AddAsync(refundTransaction); // Removed return statement
        }

        public async Task UpdateUserDepositModifiedAtAsync(int depositId, DateTime modifiedAt)
        {
            var deposit = await _context.TblUserDeposits.FindAsync(depositId);
            if (deposit != null)
            {
                deposit.ModifiedAt = modifiedAt;
                _context.TblUserDeposits.Update(deposit);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(TblUserDeposit userDeposit)
        {
            if (userDeposit == null)
                throw new ArgumentNullException(nameof(userDeposit));

            await _context.TblUserDeposits.AddAsync(userDeposit);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDepositLimitDto?> GetUserDepositLimitsAsync(int userId)
        {
            return await _context.TblUsers
                .Where(u => u.UserId == userId && u.IsDeleted == false) // Explicitly compare nullable bool to false
                .Select(u => new UserDepositLimitDto
                {
                    TotalLimit = u.TotalLimit ?? 0, // Explicitly handle nullable decimal
                    CurrentDeposit = u.Deposit ?? 0, // Explicitly handle nullable decimal
                    AvailableLimit = u.AvailableLimit // Assuming AvailableLimit is not nullable
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(TblUserLimitAuditLog log)
        {
            await _context.Set<TblUserLimitAuditLog>().AddAsync(log);
            await _context.SaveChangesAsync(); // or handle SaveChanges through UnitOfWork if needed
        }
    }

}
