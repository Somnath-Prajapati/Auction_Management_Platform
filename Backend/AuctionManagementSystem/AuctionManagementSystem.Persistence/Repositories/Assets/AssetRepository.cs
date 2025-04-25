using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Assets
{
    public class AssetRepository : IAssetsRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AssetRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblAsset>> GetAllAsync()
        {
            return await _context.TblAssets
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Seller)
                .Include(a => a.Awarding)
                .Include(a => a.Vat)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .ToListAsync();
        }

        public async Task<TblAsset?> GetByIdAsync(int id)
        {
            return await _context.TblAssets
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Seller)
                .Include(a => a.Awarding)
                .Include(a => a.Vat)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .FirstOrDefaultAsync(a => a.AssetId == id);
        }

        public async Task<TblAsset> AddAsset(TblAsset asset)
        {
            _context.TblAssets.Add(asset);
            await _context.SaveChangesAsync();

            return await _context.TblAssets
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Seller)
                .Include(a => a.Awarding)
                .Include(a => a.Vat)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .FirstOrDefaultAsync(a => a.AssetId == asset.AssetId);  
        }

        public async Task UpdateAsync(TblAsset asset)
        {
            var a = _context.TblAssets.Update(asset);
            Console.WriteLine(a);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TblAsset asset)
        {
            _context.TblAssets.Remove(asset);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AssetsIsExist(int id)
        {
            return await _context.TblAssets.AnyAsync(a => a.AssetId == id);
        }

        public async Task<IEnumerable<TblAsset>> SearchAsset(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Enter product name first");

            }
            var Asset =  await _context.TblAssets
                .Include(a => a.Category)
                .Include(a => a.Status)
                .Include(a => a.Seller)
                .Include(a => a.Awarding)
                .Include(a => a.Vat)
                .Include(a => a.TblAssetGalleries)
                .Include(a => a.TblAssetDocuments)
                .Include(a => a.TblAssetDetails)
                .Where(a => a.Title.Contains(name))
                .ToListAsync();

            var terms = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return Asset.Where(
                p => terms.Any(term => p.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                ||
                p.Description.Contains(term, StringComparison.OrdinalIgnoreCase)));
        }

       
    }
}
