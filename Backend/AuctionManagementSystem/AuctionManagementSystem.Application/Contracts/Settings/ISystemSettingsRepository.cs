using AuctionManagementSystem.Domain.Entities.Settings;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Contracts.Settings
{
    public interface ISystemSettingsRepository
    {
        Task<TblSystemSetting> GetSystemSettingsAsync();
        Task<TblSystemSetting> GetSystemSettingsByIdAsync(int id);
        Task<TblSystemSetting> CreateSystemSettingsAsync(TblSystemSetting settings);
        Task<TblSystemSetting> UpdateSystemSettingsAsync(TblSystemSetting settings);
        Task<bool> DeleteSystemSettingsAsync(int id);

        Task<TblSystemSetting?> GetByIdAsync(int id);
    }
}
