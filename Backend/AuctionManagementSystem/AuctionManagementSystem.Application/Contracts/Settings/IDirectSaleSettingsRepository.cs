using AuctionManagementSystem.Domain.Entities.Settings;

namespace AuctionManagementSystem.Domain.Interfaces
{
    public interface IDirectSaleSettingsRepository
    {
        Task<TblDirectSaleSetting> GetByIdAsync(int id);
        Task<List<TblDirectSaleSetting>> GetAllAsync();
        Task AddAsync(TblDirectSaleSetting entity);
        Task UpdateAsync(TblDirectSaleSetting entity);
        Task DeleteAsync(TblDirectSaleSetting entity, CancellationToken cancellationToken);

    }
}
