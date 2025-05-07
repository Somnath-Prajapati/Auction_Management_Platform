using AuctionManagementSystem.Domain.Entities.Settings;

namespace AuctionManagementSystem.Application.Interfaces.Repositories
{
    public interface IStaticPagesSettingsRepository
    {
        Task<TblStaticPagesSettingDto?> GetByIdAsync(int id);
        Task<IEnumerable<TblStaticPagesSettingDto>> GetAllAsync();
        Task<TblStaticPagesSettingDto> AddAsync(TblStaticPagesSettingDto entity);
        Task UpdateAsync(TblStaticPagesSettingDto entity);
        Task DeleteAsync(TblStaticPagesSettingDto entity);
    }
}
