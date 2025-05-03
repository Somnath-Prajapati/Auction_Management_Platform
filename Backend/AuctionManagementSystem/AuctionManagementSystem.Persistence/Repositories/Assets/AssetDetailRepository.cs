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
    public class AssetDetailRepository : IAssetDetailRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AssetDetailRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddAssetDetailsAsync(IEnumerable<TblAssetDetail> assetDetails)
        {
            await _context.TblAssetDetails.AddRangeAsync(assetDetails);
            await _context.SaveChangesAsync();

        }

        public async Task<TblAssetDetail> GetDetailsByIdAsync(int id)
        {
           return await _context.TblAssetDetails.FindAsync(id);
        }

        public async Task<IEnumerable<TblAssetDetail>> GetDetailsAsync()
        {
            return await _context.TblAssetDetails
                .Include(d=> d.Asset)             
                .ToListAsync();
        }

        public async Task<int> AddAsync(TblAssetDetail assetDetail)
        {
            _context.TblAssetDetails.Add(assetDetail);
            await _context.SaveChangesAsync();
            return assetDetail.DetailId;  
        }
    }
}
