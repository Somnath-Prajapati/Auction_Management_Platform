using AuctionManagementSystem.Application.Interfaces.Repositories;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

public class StaticPagesSettingsRepository : IStaticPagesSettingsRepository
{
    private readonly AuctionManagementDbContext _context;

    public StaticPagesSettingsRepository(AuctionManagementDbContext context)
    {
        _context = context;
    }

    public async Task<TblStaticPagesSettingDto?> GetByIdAsync(int id)
        => await _context.TblStaticPagesSettings.FindAsync(id);

    public async Task<IEnumerable<TblStaticPagesSettingDto>> GetAllAsync()
        => await _context.TblStaticPagesSettings.ToListAsync();

    public async Task<TblStaticPagesSettingDto> AddAsync(TblStaticPagesSettingDto entity)
    {
        _context.TblStaticPagesSettings.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TblStaticPagesSettingDto entity)
    {
        // Fix: Removed the undefined 'obj' and directly updated the entity.
        var existingEntity = await _context.TblStaticPagesSettings.FindAsync(entity.Id);
        if (existingEntity != null)
        {
            existingEntity.PrivacyPolicy = entity.PrivacyPolicy;
            existingEntity.TermsAndConditions = entity.TermsAndConditions;
            existingEntity.CookiesPolicy = entity.CookiesPolicy;

            _context.TblStaticPagesSettings.Update(existingEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(TblStaticPagesSettingDto entity)
    {
        _context.TblStaticPagesSettings.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
