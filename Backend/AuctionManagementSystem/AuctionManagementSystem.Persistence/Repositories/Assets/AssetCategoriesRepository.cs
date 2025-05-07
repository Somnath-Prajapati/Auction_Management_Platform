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
    public class AssetCategoriesRepository : IAssetCategoriesRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AssetCategoriesRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }
        public async Task<TblAssetCategory?> GetByIdAsync(int id)
        {
            return await _context.TblAssetCategories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<List<TblAssetCategory>> GetAllAsync()
        {
            return await _context.TblAssetCategories
                                 .Where(c => !c.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<TblAssetCategory> AddAsync(TblAssetCategory assetCategory)
        {
            await _context.TblAssetCategories.AddAsync(assetCategory);
            await _context.SaveChangesAsync();
            return assetCategory;
        }
        public void Update(TblAssetCategory category)
        {
            _context.TblAssetCategories.Update(category);
        }

        public async Task<TblAssetCategory?> GetByNameAsync(string categoryName)
        {
            return await _context.TblAssetCategories
                .FirstOrDefaultAsync(c => c.CategoryName.ToLower() == categoryName.ToLower());
        }


    }
}
