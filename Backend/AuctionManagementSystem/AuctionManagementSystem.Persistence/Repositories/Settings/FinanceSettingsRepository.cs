using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Application.Contracts.Settings;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Settings
{
    public class FinanceSettingsRepository : IFinanceSettingsRepository
    {
        private readonly AuctionManagementDbContext _context;

        public FinanceSettingsRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<TblFinanceSetting> AddAsync(TblFinanceSetting setting, CancellationToken cancellationToken)
        {
            _context.TblFinanceSettings.Add(setting);
            await _context.SaveChangesAsync(cancellationToken);
            return setting;
        }

        public async Task<TblFinanceSetting?> GetByIdAsync(int id) =>
            await _context.TblFinanceSettings.FindAsync(id);

        public async Task UpdateAsync(TblFinanceSetting settings)
        {
            _context.TblFinanceSettings.Update(settings);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TblFinanceSetting settings)
        {
            _context.TblFinanceSettings.Remove(settings);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TblFinanceSetting>> GetAllAsync()
        {
            return await _context.TblFinanceSettings.ToListAsync();
        }

    }

}
