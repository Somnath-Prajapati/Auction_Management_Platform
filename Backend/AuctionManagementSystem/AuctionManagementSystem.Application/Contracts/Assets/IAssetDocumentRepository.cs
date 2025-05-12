using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetDocumentRepository
    {
        Task<int> AddAsync(TblAssetDocument entity);
        Task<bool> UpdateAsync(int id, TblAssetDocument entity);
        Task<bool> DeleteAsync(int id);
        Task<TblAssetDocument?> GetByIdAsync(int id);
        Task<IEnumerable<TblAssetDocument>> GetAllAsync();


        Task DeleteDocumentsByAssetIdAsync(int assetId); //for deleting the document for adding asset
    }
}
