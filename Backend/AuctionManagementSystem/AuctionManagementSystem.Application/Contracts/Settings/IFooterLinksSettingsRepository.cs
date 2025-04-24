using AuctionManagementSystem.Domain.Entities.Settings;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Repositories
{
    public interface IFooterLinksSettingsRepository
    {
        Task<TblFooterLinksSetting> GetAsync();
        Task<TblFooterLinksSetting> GetByIdAsync(int id);
        Task<TblFooterLinksSetting> CreateAsync(TblFooterLinksSetting entity);
        Task<TblFooterLinksSetting> UpdateAsync(TblFooterLinksSetting entity);
        Task<bool> DeleteAsync(int id);
    }
}
