using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
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





        public async Task<GetAssetDetailsDto> GetDetailsByIdAsync(int id)
        {
            //return await _context.TblAssetDetails.FindAsync(id);
            var assetDetails = await _context.TblAssetDetails
                .Where(a => a.AssetId == id)
                .Select(a => new AssetDetailDtosA
                {
                    AttributeName = a.AttributeName,
                    AttributeValue = a.AttributeValue
                })
                .ToListAsync();

            if (assetDetails == null || assetDetails.Count == 0)
            {
                return new GetAssetDetailsDto
                {
                    AssetId = id,
                    AssetDetails = new List<AssetDetailDtosA>()
                };
            }

            return new GetAssetDetailsDto
            {
                AssetId = id,
                AssetDetails = assetDetails
            };

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



        public async Task UpdateDetailsAsync(int assetId, List<UpdateAssetDetailDto> updatedDetails)
        {
            
            updatedDetails ??= new List<UpdateAssetDetailDto>(); 

            
            var existingDetails = await _context.TblAssetDetails
                .Where(d => d.AssetId == assetId)
                .ToListAsync();

            
            if (existingDetails.Any())
            {
                _context.TblAssetDetails.RemoveRange(existingDetails);
            }

           
            if (updatedDetails.Any())
            {
                var newDetails = updatedDetails.Select(detail => new TblAssetDetail
                {
                    AssetId = assetId,
                    AttributeName = detail.AttributeName,
                    AttributeValue = detail.AttributeValue
                });

                await _context.TblAssetDetails.AddRangeAsync(newDetails);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAssetDetailsAsync(int assetId)
        {
            var assetDetails = await _context.TblAssetDetails
                .Where(d => d.AssetId == assetId)
                .ToListAsync();

            _context.TblAssetDetails.RemoveRange(assetDetails);
        }
    }
}
