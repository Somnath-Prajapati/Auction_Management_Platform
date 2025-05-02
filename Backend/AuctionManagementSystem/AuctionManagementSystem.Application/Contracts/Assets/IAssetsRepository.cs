using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetsRepository
    {

        Task<IEnumerable<GetAssetsFormDto>> GetAllAsync();  
        Task<GetAssetsFormDto> GetByIdAsync(int id);

        Task<TblAsset> GetIdDeleteAsync(int id);
        Task<TblAsset> AddAsset(TblAsset asset);
        Task DeleteAsync(TblAsset asset);
        Task UpdateAsync(TblAsset asset);
        Task<bool> AssetsIsExist(int id);
        Task<IEnumerable<TblAsset>> SearchAsset(string name);



        //Task<int> AddAssetWithMediaAsync(TblAsset asset, List<TblAssetGallery> galleries, List<TblAssetDocument> documents);

    }
}
