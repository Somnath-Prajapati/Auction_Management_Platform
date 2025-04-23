using AuctionManagementSystem.Domain.Entities.Settings;

namespace AuctionManagementSystem.Application.Interfaces.Repositories
{
    public interface IStaticPagesSettingsRepository
    {
        Task<TblStaticPagesSetting?> GetByIdAsync(int id);
        Task<IEnumerable<TblStaticPagesSetting>> GetAllAsync();
        Task<TblStaticPagesSetting> AddAsync(TblStaticPagesSetting entity);
        Task UpdateAsync(TblStaticPagesSetting entity);
        Task DeleteAsync(TblStaticPagesSetting entity);
    }
}
