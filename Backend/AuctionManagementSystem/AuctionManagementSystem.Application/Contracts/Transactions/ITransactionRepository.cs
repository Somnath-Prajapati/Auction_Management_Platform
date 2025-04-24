
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Application.Contracts.Transactions
{
    public interface ITransactionRepository
    {
        Task<TblTransaction?> GetByIdAsync(int id);
        Task<List<TblTransaction>> GetAllAsync();
        Task<TblTransaction> AddAsync(TblTransaction entity);
        Task UpdateAsync(TblTransaction entity);
        Task DeleteAsync(TblTransaction entity);
    }
}