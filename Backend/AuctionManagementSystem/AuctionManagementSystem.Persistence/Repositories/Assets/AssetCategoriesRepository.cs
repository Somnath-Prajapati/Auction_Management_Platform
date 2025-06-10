using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Translations;
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
            return await _context.TblAssetCategories
                                 .Include(c => c.TblAssetCategoryPaymentMethods)
                                 .ThenInclude(cp => cp.PaymentMethod)
                                 .FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);
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
            await _context.TblAssetCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Category created with CategoryId: {category.CategoryId}");

            foreach (var methodId in paymentMethodIds)
            {
                var relation = new TblAssetCategoryPaymentMethod
                {
                    CategoryId = category.CategoryId,
                    PaymentMethodId = methodId
                };
                Console.WriteLine($"Adding PaymentMethod: {methodId} for CategoryId: {category.CategoryId}");
                _context.TblAssetCategoryPaymentMethods.Add(relation);
            }

            await _context.SaveChangesAsync();
            return category;
        }
        public async Task UpdateWithPaymentMethodsAsync(TblAssetCategory category, List<int> newPaymentMethodIds)
        {
            var existing = await _context.TblAssetCategories
                .Include(c => c.TblAssetCategoryPaymentMethods)
                .FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            if (existing == null)
                throw new Exception("Category not found");

            _context.Entry(existing).CurrentValues.SetValues(category);

            _context.TblAssetCategoryPaymentMethods
                .RemoveRange(existing.TblAssetCategoryPaymentMethods);

            foreach (var methodId in newPaymentMethodIds)
            {
                existing.TblAssetCategoryPaymentMethods.Add(new TblAssetCategoryPaymentMethod
                {
                    CategoryId = existing.CategoryId,
                    PaymentMethodId = methodId
                });
            }

            await _context.SaveChangesAsync();
        }
        public async Task<List<AuctionCategoryTranslationDto>> GetTranslationsByLangCodeAsync(string langCode)
        {
            return await _context.TblAuctionCategoriesTranslations
                .Where(t => t.Language.Code == langCode)
                .Select(t => new AuctionCategoryTranslationDto
                {
                    CategoryId = t.CategoryId,
                    TranslatedName = t.TranslatedName
                }).ToListAsync();
        }

        public async Task<List<AssetCategoryTranslationDto>> GetAssetCategoryTranslationsByLangCodeAsync(string langCode)
        {
            return await _context.TblAssetCategoryTranslations
                .Where(t => t.Language.Code == langCode)
                .Select(t => new AssetCategoryTranslationDto
                {
                    CategoryId = t.CategoryId,
                    TranslatedCategoryName = t.TranslatedCategoryName,
                    TranslatedSubcategory = t.TranslatedSubcategory,
                    TranslatedDetails = t.TranslatedDetails
                }).ToListAsync();
        }


    }
}
