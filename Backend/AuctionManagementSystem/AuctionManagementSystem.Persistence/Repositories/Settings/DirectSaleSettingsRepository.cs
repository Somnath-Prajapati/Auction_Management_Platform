using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Domain.Interfaces;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories
{
    public class DirectSaleSettingsRepository : IDirectSaleSettingsRepository
    {
        private readonly AuctionManagementDbContext _context;

        public DirectSaleSettingsRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<TblDirectSaleSetting> GetByIdAsync(int id)
        {
            return await _context.TblDirectSaleSettings
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TblDirectSaleSetting>> GetAllAsync()
        {
            return await _context.TblDirectSaleSettings.ToListAsync();
        }

        public async Task AddAsync(TblDirectSaleSetting entity)
        {
            await _context.TblDirectSaleSettings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TblDirectSaleSetting entity)
        {
            _context.TblDirectSaleSettings.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TblDirectSaleSetting entity, CancellationToken cancellationToken)
        {
            _context.TblDirectSaleSettings.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
