using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetsRepository
    {

        Task<IEnumerable<GetAssetsFormDto>> GetAllAsync();
        Task<List<GetAssetsFormDto>> GetDirectAllAsync(Expression<Func<TblAsset, bool>> predicate);
        Task<List<GetAssetsFormDto>> GetAuctionAllAsync(Expression<Func<TblAsset, bool>> predicate);

        Task<GetAssetsFormDto> GetByIdAsync(int id);

        Task<GetAssetsFormDto> GetByIdViewAsync(int id, string languageId);

        Task<TblAsset> GetIdDeleteAsync(int id);
        Task<TblAsset> AddAsset(TblAsset asset);

        Task<TblAsset> AddAssetForGallery(TblAsset asset);
        Task DeleteAsync(TblAsset asset);
        Task UpdateAsync(TblAsset asset);
        Task<bool> AssetsIsExist(int id);
        Task<IEnumerable<TblAsset>> SearchAsset(string name);

        Task<bool> HasAnyDirectAndActiveAuctionAsync(int auctionIds); // this is for directsale
        Task DeactivateExpiredAssetsBasedOnDeadlineAsync();        // added for hangfire 

        //Task<int> AddAssetWithMediaAsync(TblAsset asset, List<TblAssetGallery> galleries, List<TblAssetDocument> documents);

    }
}
