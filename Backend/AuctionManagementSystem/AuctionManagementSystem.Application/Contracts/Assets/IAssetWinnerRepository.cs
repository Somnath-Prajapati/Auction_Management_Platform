using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Application.Contracts.Assets
{
    public interface IAssetWinnerRepository
    {
        Task AddAsync(TblAssetWinner winner);
        Task<List<TblAssetWinner>> GetByUserIdAsync(int userId);
        Task<List<TblAssetWinner>> GetUnseenWinsByUserIdAsync(int userId);
        Task<int> MarskAsSeenAsync(int userId);
    }
}
