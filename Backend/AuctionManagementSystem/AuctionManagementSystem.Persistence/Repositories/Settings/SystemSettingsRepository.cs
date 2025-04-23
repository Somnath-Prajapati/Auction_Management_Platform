using AuctionManagementSystem.Application.Contracts.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Settings
{
    public class SystemSettingsRepository : ISystemSettingsRepository
    {
        private readonly AuctionManagementDbContext _context;

        public SystemSettingsRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<TblSystemSetting> GetSystemSettingsAsync()
        {
            return await _context.TblSystemSettings.FirstOrDefaultAsync() ?? new TblSystemSetting();
        }

        public async Task<TblSystemSetting?> GetSystemSettingsByIdAsync(int id)
        {
            return await _context.TblSystemSettings.FindAsync(id);
        }

        public async Task<TblSystemSetting?> GetByIdAsync(int id)
        {
            return await _context.TblSystemSettings.FindAsync(id);
        }

        public async Task<TblSystemSetting> CreateSystemSettingsAsync(TblSystemSetting settings)
        {
            _context.TblSystemSettings.Add(settings);
            await _context.SaveChangesAsync();
            return settings;
        }

        public async Task<TblSystemSetting> UpdateSystemSettingsAsync(TblSystemSetting settings)
        {
            _context.TblSystemSettings.Update(settings);
            await _context.SaveChangesAsync();
            return settings;
        }

        public async Task<bool> DeleteSystemSettingsAsync(int id)
        {
            var entity = await _context.TblSystemSettings.FindAsync(id);
            if (entity == null)
                return false;

            _context.TblSystemSettings.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
