using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetsRepository
    {
        Task<IEnumerable<TblAsset>> GetAllAsync();
        Task<TblAsset> GetByIdAsync(int id);
        Task<TblAsset> AddAsset(TblAsset asset);
        Task DeleteAsync(TblAsset asset);
        Task UpdateAsync(TblAsset asset);
        Task<bool> AssetsIsExist(int id);
        Task<IEnumerable<TblAsset>> SearchAsset(string name);
        
    }
}
