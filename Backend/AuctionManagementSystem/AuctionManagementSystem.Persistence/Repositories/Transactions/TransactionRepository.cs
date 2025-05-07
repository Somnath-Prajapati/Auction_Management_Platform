using System.Data;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Domain.Entities.Transaction;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AuctionManagementDbContext _context;

    public TransactionRepository(AuctionManagementDbContext context)
    {
        _context = context;
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


    public async Task<TblTransaction> AddAsync(TblTransaction entity)
   {
        //entity.TransactionNumber = Guid.NewGuid().ToString(); // Or use a custom format
        entity.TransactionNumber = await GetTransactionNumberFromDbAsync();
        await _context.TblTransactions.AddAsync(entity);
        await _context.SaveChangesAsync();
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
}
