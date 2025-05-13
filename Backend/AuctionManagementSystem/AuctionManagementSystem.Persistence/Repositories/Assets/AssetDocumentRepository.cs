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
    public class AssetDocumentRepository : IAssetDocumentRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AssetDocumentRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(TblAssetDocument entity)
        {
            _context.TblAssetDocuments.Add(entity);
            await _context.SaveChangesAsync();
            return entity.DocumentId;
        }

        public async Task<bool> UpdateAsync(int id, TblAssetDocument entity)
        {
            var existing = await _context.TblAssetDocuments.FindAsync(id);
            if (existing == null) return false;

            existing.AssetId = entity.AssetId;
            existing.DocumentType = entity.DocumentType;
            existing.FilePath = entity.FilePath;

            _context.TblAssetDocuments.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TblAssetDocuments.FindAsync(id);
            if (entity == null) return false;

            _context.TblAssetDocuments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteDocumentsByAssetIdAsync(int assetId)
        {
            // Fetch all documents related to the given assetId
            var documents = await _context.TblAssetDocuments
                .Where(d => d.AssetId == assetId)
                .ToListAsync();

            // If there are any documents to delete, remove them
            if (documents.Any())
            {
                _context.TblAssetDocuments.RemoveRange(documents);
                await _context.SaveChangesAsync();
            }
        }




        public async Task<TblAssetDocument?> GetByIdAsync(int id)
        {
            return await _context.TblAssetDocuments
                .Include(d => d.Asset)
                .FirstOrDefaultAsync(d => d.DocumentId == id);
        }

        public async Task<IEnumerable<TblAssetDocument>> GetAllAsync()
        {
            return await _context.TblAssetDocuments
                .Include(d => d.Asset)
                .ToListAsync();
        }
    }
}
