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

    public async Task<TblStaticPagesSetting?> GetByIdAsync(int id)
        => await _context.TblStaticPagesSettings.FindAsync(id);

    public async Task<IEnumerable<TblStaticPagesSetting>> GetAllAsync()
        => await _context.TblStaticPagesSettings.ToListAsync();

    public async Task<TblStaticPagesSetting> AddAsync(TblStaticPagesSetting entity)
    {
        _context.TblStaticPagesSettings.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TblStaticPagesSetting entity)
    {
        _context.TblStaticPagesSettings.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TblStaticPagesSetting entity)
    {
        _context.TblStaticPagesSettings.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
