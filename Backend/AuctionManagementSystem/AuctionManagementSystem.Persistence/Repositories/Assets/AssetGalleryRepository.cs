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
    public class AssetGalleryRepository : IAssetGalleryRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AssetGalleryRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(TblAssetGallery gallery)
        {
            _context.TblAssetGalleries.Add(gallery);
            await _context.SaveChangesAsync();
            return gallery.GalleryId;
        }

        public async Task<bool> UpdateAsync(int id, TblAssetGallery updated)
        {
            var existing = await _context.TblAssetGalleries.FindAsync(id);
            if (existing == null) return false;

            existing.AssetId = updated.AssetId;
            existing.FilePath = updated.FilePath;
            existing.SortOrder = updated.SortOrder;
            existing.MediaType = updated.MediaType;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gallery = await _context.TblAssetGalleries.FindAsync(id);
            if (gallery == null) return false;

            _context.TblAssetGalleries.Remove(gallery);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TblAssetGallery?> GetByIdAsync(int id)
        {
            return await _context.TblAssetGalleries.FindAsync(id);
        }

        public async Task<IEnumerable<TblAssetGallery>> GetAllAsync()
        {
            return await _context.TblAssetGalleries.ToListAsync();
        }

        public async Task<IEnumerable<TblAssetGallery>> GetByAssetIdAsync(int assetId)
        {
            return await _context.TblAssetGalleries
                .Where(g => g.AssetId == assetId)
                .ToListAsync();
        }
    }
}
