using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetDetailRepository
    {
        Task AddAssetDetailsAsync(IEnumerable<TblAssetDetail> assetDetails);

        Task<GetAssetDetailsDto> GetDetailsByIdAsync(int id);

        Task<IEnumerable<TblAssetDetail>> GetDetailsAsync();

        Task UpdateDetailsAsync(int assetId, List<UpdateAssetDetailDto> updatedDetails); /// for updateasset

        Task<int> AddAsync(TblAssetDetail assetDetail);  //after the changes i have added while adding whole form at once
        Task RemoveAssetDetailsAsync(int assetId);
    }

}
