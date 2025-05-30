using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;

namespace AuctionManagementSystem.Persistence.Repositories.Assets
{
    public class AssetWinnerRepository : IAssetWinnerRepository
    {
        private readonly AuctionManagementDbContext _context;
        public AssetWinnerRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TblAssetWinner winner)
        {
            await _context.TblAssetWinners.AddAsync(winner);
            //await _context.SaveChangesAsync();// because running multipletimes in loop
        }
    }
}
