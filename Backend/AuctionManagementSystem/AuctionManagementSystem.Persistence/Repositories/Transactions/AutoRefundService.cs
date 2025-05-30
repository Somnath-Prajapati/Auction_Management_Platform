using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Persistence.Repositories.Transactions
{
   

    public class AutoRefundService : IAutoRefundService
    {
        private readonly AuctionManagementDbContext _context;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ILogger<AutoRefundService> _logger;
        private readonly ICurrentUserService _systemUser;

        private readonly TimeSpan _unusedPeriod = TimeSpan.FromDays(180);

        public AutoRefundService(
            AuctionManagementDbContext context,
            ITransactionRepository transactionRepository,
            IAuditTrailService auditTrailService,
            ILogger<AutoRefundService> logger,
            ICurrentUserService currentUser)
        {
            _context = context;
            _transactionRepository = transactionRepository;
            _auditTrailService = auditTrailService;
            _logger = logger;
            _systemUser = currentUser;
        }

        public async Task ProcessAutoRefundsAsync()
        {
            var cutoffDate = DateTime.UtcNow.Subtract(_unusedPeriod);

            var depositsToRefund = await _context.TblTransactions
                .Include(t => t.User)
                //.Where(t =>
                //    t.TransactionType.TransactionTypeName == "Deposit" &&
                //    t.Status.StatusName == "Approved" &&
                //    t.CreatedAt <= cutoffDate &&
                //    !t.IsRefunded &&
                //    !t.IsDeleted)
                .ToListAsync();

            foreach (var deposit in depositsToRefund)
            {
                try
                {
                    var refundTransaction = new TblTransaction
                    {
                        UserId = deposit.UserId,
                        Amount = deposit.Amount,
                        TransactionTypeId = await GetRefundTransactionTypeIdAsync(),
                        StatusId = await GetApprovedStatusIdAsync(),
                        PaymentMethodId = deposit.PaymentMethodId,
                        CardTypeId = deposit.CardTypeId,
                        CreatedAt = DateTime.UtcNow,
                        TransactionNumber = await _transactionRepository.GetTransactionNumberFromDbAsync(),
                        Notes = $"Auto refund for unused deposit transaction #{deposit.TransactionNumber}",
                        //IsRefund = true,
                        IsDeleted = false
                    };

                    await _transactionRepository.AddAsync(refundTransaction);

                    deposit.User.Deposit -= refundTransaction.Amount;
                    //deposit.IsRefunded = true;

                    await _context.SaveChangesAsync();

                    await _auditTrailService.LogChangeAsync(
                        userId: _systemUser.UserId,
                        username: _systemUser.Username,
                        roleName: _systemUser.RoleName,
                        modelName: "Transaction",
                        changeType: "AutoRefund",
                        recordId: refundTransaction.TransactionId,
                        beforeChange: null,
                        afterChange: JsonConvert.SerializeObject(refundTransaction, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));

                    _logger.LogInformation("Auto refund created for deposit transaction {DepositId}, refund transaction {RefundId}",
                        deposit.TransactionId, refundTransaction.TransactionId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during auto refund for deposit transaction {DepositId}", deposit.TransactionId);
                }
            }
        }

        private async Task<int> GetRefundTransactionTypeIdAsync()
        {
            var refundType = await _context.TblTransactionTypes.FirstOrDefaultAsync(t => t.TransactionTypeName == "Refund");
            if (refundType == null)
                throw new InvalidOperationException("Refund transaction type not found.");
            return refundType.TransactionTypeId;
        }

        private async Task<int> GetApprovedStatusIdAsync()
        {
            var approvedStatus = await _context.TblTransactionStatuses.FirstOrDefaultAsync(s => s.StatusName == "Approved");
            if (approvedStatus == null)
                throw new InvalidOperationException("Approved status not found.");
            return approvedStatus.StatusId;
        }
    }

}
