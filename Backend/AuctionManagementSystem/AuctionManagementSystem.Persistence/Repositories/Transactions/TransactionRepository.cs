using System.Data;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Domain.Models;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuctionManagementSystem.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AuctionManagementDbContext _context;
    private readonly ILogger<TransactionRepository> _logger;

    public TransactionRepository(AuctionManagementDbContext context, ILogger<TransactionRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<string> GetTransactionNumberFromDbAsync()
    {
        using var command = _context.GetDbConnection().CreateCommand();
        command.CommandText = "GenerateTransactionNumber";
        command.CommandType = CommandType.StoredProcedure;

        if (command.Connection.State != ConnectionState.Open)
            await command.Connection.OpenAsync();

        var result = await command.ExecuteReaderAsync();
        string transactionNumber = null;

        if (await result.ReadAsync())
        {
            transactionNumber = result.GetString(0);
        }

        await result.CloseAsync();
        return transactionNumber;
    }



    public async Task<TblTransaction?> GetByIdAsync(int id)
    {
        return await _context.TblTransactions
            .Include(t => t.User)
            .Include(t => t.PaymentMethod)
            .Include(t => t.Status)
            .Include(t => t.TransactionType)
            .FirstOrDefaultAsync(t => t.TransactionId == id);
    }


    public async Task<TblTransaction> GetTransactionByIdAsync(int transactionId)
    {
        return await _context.TblTransactions
            .Include(t => t.User)  // Ensure that related user data is included
            .Include(t => t.PaymentMethod)
            .Include(t => t.Status)
            .Include(t => t.TransactionType)
            .Include(t=>t.CardType)
            .Where(t => t.TransactionId == transactionId)
            .FirstOrDefaultAsync();
    }


    public async Task<List<TblTransaction>> GetAllAsync()
    {
        return await _context.TblTransactions
            .Include(t => t.User)
            .Include(t => t.PaymentMethod)
            .Include(t => t.Status)
            .Include(t => t.TransactionType)
            .ToListAsync();
    }

    public async Task<List<TblTransaction>> GetAllWithDetailsAsync(CancellationToken cancellationToken)
    {
        //return await _context.TblTransactions
        //    .Include(t => t.PaymentMethod)
        //    .Include(t => t.Status)
        //    .Include(t => t.TransactionType)
        //    .Include(t => t.CardType)
        //    .ToListAsync(cancellationToken);

        return await _context.TblTransactions
        .Where(t => !t.IsDeleted)
        .Include(t => t.User)
        .Include(t => t.TransactionType)
        .Include(t => t.PaymentMethod)
        .Include(t => t.CardType)
        .Include(t => t.Status)
        .ToListAsync(cancellationToken);
    }

    public async Task<string?> GetDocumentPathByTransactionIdAsync(int transactionId, CancellationToken cancellationToken)
    {
        return await _context.TblTransactionDocuments
            .Where(d => d.TransactionId == transactionId)
            .Select(d => d.FilePath)
            .FirstOrDefaultAsync(cancellationToken);
    }



    public async Task<TblTransaction> AddAsync(TblTransaction entity)
    {
        // Generate Transaction Number
        entity.TransactionNumber = await GetTransactionNumberFromDbAsync();
        // Add transaction to DB
        await _context.TblTransactions.AddAsync(entity);


        await _context.SaveChangesAsync();

        // === Handle Approved Deposit Logic ===
        if (entity.TransactionTypeId==2 && entity.StatusId == 2)
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserId == entity.UserId);
            if (user != null)
            {
                decimal oldDeposit = user.Deposit ?? 0m;
                decimal oldTotalLimit = user.TotalLimit ?? 0m;
                decimal oldAvailableLimit = user.AvailableLimit;

                decimal newDeposit = oldDeposit + entity.Amount;
                decimal newTotalLimit = newDeposit * 10;
                decimal usedLimit = oldTotalLimit - oldAvailableLimit;
                if (usedLimit < 0) usedLimit = 0;

                decimal newAvailableLimit = newTotalLimit - usedLimit;
                if (newAvailableLimit < 0) newAvailableLimit = 0;

                // Apply updates to user
                user.Deposit = newDeposit;
                user.TotalLimit = newTotalLimit;
                user.AvailableLimit = newAvailableLimit;

                // Audit Log
                var auditLog = new TblUserLimitAuditLog
                {
                    UserId = user.UserId,
                    ActionType = "Deposit",
                    OldDeposit = oldDeposit,
                    NewDeposit = newDeposit,
                    OldTotalLimit = oldTotalLimit,
                    NewTotalLimit = newTotalLimit,
                    OldAvailableLimit = oldAvailableLimit,
                    NewAvailableLimit = newAvailableLimit,
                    Notes = $"Deposit transaction ID {entity.TransactionId} applied.",
                    ChangedBy = entity.UpdatedBy,
                    ChangedDate = DateTime.UtcNow
                };

                _context.TblUserLimitAuditLogs.Add(auditLog);

                await _context.SaveChangesAsync(); // Save user + audit changes
            }
        }

        return entity;
    }


    public async Task<TblTransaction> GetTransactionWithDetailsAsync(int transactionId)
    {
        // Eager load related entities using Include
        return await _context.TblTransactions
            .Include(x => x.User)
            .Include(x => x.TransactionType)
            .Include(x => x.PaymentMethod)
            .Include(x => x.CardType)
            .Include(x => x.Status)
            .Include(x => x.TblTransactionDocuments)
            .FirstOrDefaultAsync(x => x.TransactionId == transactionId);
    }


    public async Task UpdateAsync(TblTransaction entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TblTransaction entity)
    {
        _context.TblTransactions.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task HandleDepositAdjustmentOnStatusChangeAsync(TblTransaction before, TblTransaction after)
    {
        bool statusChangedToApproved = before.StatusId == 1 && after.StatusId == 2;
        bool isDepositOrRefund = after.TransactionType?.TransactionTypeName == "Deposit"
                                  || after.TransactionType?.TransactionTypeName == "Refund";

        if (!statusChangedToApproved || !isDepositOrRefund)
            return;

        var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserId == after.UserId);
        if (user == null)
        {
            _logger.LogWarning("User not found for Transaction ID {TransactionId}", after.TransactionId);
            return;
        }

        decimal oldDeposit = user.Deposit ?? 0m;
        decimal oldTotalLimit = user.TotalLimit ?? 0m;
        decimal oldAvailableLimit = user.AvailableLimit;

        // Adjust deposit
        decimal newDeposit = after.TransactionType.TransactionTypeName == "Deposit"
            ? oldDeposit + after.Amount
            : oldDeposit - after.Amount;

        if (newDeposit < 0) newDeposit = 0;

        // Recalculate limits
        decimal newTotalLimit = newDeposit * 10;
        decimal usedLimit = oldTotalLimit - oldAvailableLimit;
        if (usedLimit < 0) usedLimit = 0;

        decimal newAvailableLimit = newTotalLimit - usedLimit;
        if (newAvailableLimit < 0) newAvailableLimit = 0;

        // Apply updates
        user.Deposit = newDeposit;
        user.TotalLimit = newTotalLimit;
        user.AvailableLimit = newAvailableLimit;

        // Create audit log
        var auditLog = new TblUserLimitAuditLog
        {
            UserId = user.UserId,
            ActionType = after.TransactionType.TransactionTypeName,
            OldDeposit = oldDeposit,
            NewDeposit = newDeposit,
            OldTotalLimit = oldTotalLimit,
            NewTotalLimit = newTotalLimit,
            OldAvailableLimit = oldAvailableLimit,
            NewAvailableLimit = newAvailableLimit,
            Notes = $"Transaction ID {after.TransactionId} processed.",
            ChangedBy = after.UpdatedBy
        };

        _context.TblUserLimitAuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User ID {UserId}'s deposit and limits updated due to transaction ID {TransactionId}.",
            user.UserId, after.TransactionId);
    }

    public async Task<List<UserTransactionDto>> GetUserTransactionsAsync(int userId)
    {
        int CompletedStatusId = 2;

        var transactions = await _context.TblTransactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new UserTransactionDto
            {
                RefNo = t.TransactionNumber,
                Request = t.TransactionTypeId == 1 ? "Deposit"
                        : t.TransactionTypeId == 2 && t.StatusId == 1 ? "Refund Request"
                        : t.TransactionTypeId == 2 && t.StatusId == 2 ? "Refund"
                        : "Unknown",

                DateTime = t.CreatedDate,
                Amount = t.Amount,
                Type = t.TransactionType.TransactionTypeName, // e.g., "Deposit", "Refund"
                Method = t.PaymentMethodId != null ? t.PaymentMethod.PaymentMethodName : "—",
                Status = t.Status.StatusName, // e.g., "Pending", "Completed"
                ApprovedDateTime = t.StatusId == CompletedStatusId ? t.UpdatedAt : null,
                ApprovedBy = t.UpdatedByUser != null ? t.UpdatedByUser.Name : null,
                Notes = t.Notes
            })
            .ToListAsync();

        return transactions;
    }

    public async Task AddTransactionDocumentsAsync(IEnumerable<TblTransactionDocument> documents)
    {
        await _context.TblTransactionDocuments.AddRangeAsync(documents);
        await _context.SaveChangesAsync();
    }


   


}
