
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Domain.Entities.Transaction;

namespace AuctionManagementSystem.Application.Contracts.Transactions
{
    public interface ITransactionRepository
    {
        Task<TblTransaction?> GetByIdAsync(int id);
        Task<string> GetTransactionNumberFromDbAsync();
        Task AddTransactionDocumentsAsync(IEnumerable<TblTransactionDocument> documents);

        Task<TblTransaction> GetTransactionWithDetailsAsync(int transactionId);

        Task<TblTransaction> GetTransactionByIdAsync(int transactionId);

        Task<List<TblTransaction>> GetAllAsync();
        Task<List<TblTransaction>> GetAllWithDetailsAsync(CancellationToken cancellationToken);

        Task<TblTransaction> AddAsync(TblTransaction entity);
        Task UpdateAsync(TblTransaction entity);
        Task DeleteAsync(TblTransaction entity);

        Task HandleDepositAdjustmentOnStatusChangeAsync(TblTransaction before, TblTransaction after);

        Task<List<UserTransactionDto>> GetUserTransactionsAsync(int userId);

        Task<List<AuctionMonthlyRevenueDto>> GetAuctionMonthlyRevenueAsync();



    }
}