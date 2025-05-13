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
                                 .Include(c => c.TblAssetCategoryPaymentMethods) 
                                 .ThenInclude(cp => cp.PaymentMethod)
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



         public async Task<TblAssetCategory> AddWithPaymentMethodsAsync(TblAssetCategory category, List<int> paymentMethodIds)
    {
        // Save the category first
        await _context.TblAssetCategories.AddAsync(category);
        await _context.SaveChangesAsync();

        // Add payment methods (category ID now available)
        foreach (var methodId in paymentMethodIds)
        {
            var relation = new TblAssetCategoryPaymentMethod
            {
                CategoryId = category.CategoryId,
                PaymentMethodId = methodId
            };
            _context.TblAssetCategoryPaymentMethods.Add(relation);
        }

        await _context.SaveChangesAsync();
        return category;
    }

    }
}
