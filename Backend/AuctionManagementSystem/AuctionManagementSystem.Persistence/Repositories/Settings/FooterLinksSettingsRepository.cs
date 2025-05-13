using AuctionManagementSystem.Application.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Persistence.Repositories
{
    public class FooterLinksSettingsRepository : IFooterLinksSettingsRepository
    {
        private readonly AuctionManagementDbContext _context;

        public FooterLinksSettingsRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<TblFooterLinksSetting> GetAsync()
        {
            return await _context.TblFooterLinksSettings.FirstOrDefaultAsync();
        }

        public async Task<TblFooterLinksSetting> GetByIdAsync(int id)
        {
            return await _context.TblFooterLinksSettings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TblFooterLinksSetting> CreateAsync(TblFooterLinksSetting entity)
        {
            await _context.TblFooterLinksSettings.AddAsync(entity); // Use AddAsync instead of Add
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TblFooterLinksSetting> UpdateAsync(TblFooterLinksSetting entity)
        {
            _context.TblFooterLinksSettings.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TblFooterLinksSettings.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;

            _context.TblFooterLinksSettings.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
