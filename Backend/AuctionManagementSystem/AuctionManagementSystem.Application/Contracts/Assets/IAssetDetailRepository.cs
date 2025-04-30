using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetDetailRepository
    {
        Task AddAssetDetailsAsync(IEnumerable<TblAssetDetail> assetDetails);

        Task<TblAssetDetail> GetDetailsByIdAsync(int id);

        Task<IEnumerable<TblAssetDetail>> GetDetailsAsync();
    }

}
