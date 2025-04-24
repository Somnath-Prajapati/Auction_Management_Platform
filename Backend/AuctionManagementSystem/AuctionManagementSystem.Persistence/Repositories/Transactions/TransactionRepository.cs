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

    //public async Task<TblTransaction?> GetByIdAsync(int id)
    //{
    //    return await _context.TblTransactions.FindAsync(id);
    //}

    //public async Task<List<TblTransaction>> GetAllAsync()
    //{
    //    return await _context.TblTransactions.ToListAsync();
    //}


    public async Task<TblTransaction?> GetByIdAsync(int id)
    {
        return await _context.TblTransactions
            .Include(t => t.User)
            .Include(t => t.PaymentMethod)
            .Include(t => t.Status)
            .Include(t => t.TransactionType)
            .FirstOrDefaultAsync(t => t.TransactionId == id);
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


    public async Task<TblTransaction> AddAsync(TblTransaction entity)
    {
        await _context.TblTransactions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
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
