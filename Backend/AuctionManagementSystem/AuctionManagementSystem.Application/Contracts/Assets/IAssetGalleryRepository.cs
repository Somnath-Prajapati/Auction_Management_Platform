using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetGalleryRepository
    {
        Task<int> AddAsync(TblAssetGallery gallery);
        Task<bool> UpdateAsync(int id, TblAssetGallery updated);
        Task<bool> DeleteAsync(int id);
        Task<TblAssetGallery?> GetByIdAsync(int id);
        Task<IEnumerable<TblAssetGallery>> GetAllAsync();

    }
   
}
