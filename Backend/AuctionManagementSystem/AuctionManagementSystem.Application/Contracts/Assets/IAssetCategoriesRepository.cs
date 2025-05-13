
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetCategoriesRepository
    {
        Task<TblAssetCategory?> GetByNameAsync(string categoryName);

        Task<List<TblAssetCategory>> GetAllAsync();
        Task<TblAssetCategory> AddAsync(TblAssetCategory tblAssetCategory);

        // Required for DeleteAssetCategoryHandler
        Task<TblAssetCategory?> GetByIdAsync(int id);
        void Update(TblAssetCategory tblAssetCategory);
        Task<int> SaveAsync();

        Task<TblAssetCategory> AddWithPaymentMethodsAsync(TblAssetCategory category, List<int> paymentMethodIds);
    }
}
