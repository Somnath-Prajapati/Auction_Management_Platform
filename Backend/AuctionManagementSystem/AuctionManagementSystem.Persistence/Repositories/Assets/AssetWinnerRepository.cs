using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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
            //await _context.SaveChangesAsync();//I remved it Because because running multipletimes in loop
        }


        public async Task<List<TblAssetWinner>> GetByUserIdAsync(int userId)
        {
            var Wins = await _context.TblAssetWinners.Where(w => w.UserId == userId).ToListAsync();
            return Wins;
        }

        public async Task<List<TblAssetWinner>> GetUnseenWinsByUserIdAsync(int userId)
        {
            return await _context.TblAssetWinners.Where(w => w.UserId == userId && !w.IsSeen).ToListAsync();

        }

        public async Task<int> MarskAsSeenAsync(int userId)
        {
            var wins = await _context.TblAssetWinners.Where(w => w.UserId == userId && !w.IsSeen).ToListAsync();
            foreach (var win in wins)
            {
                win.IsSeen = true;
            }
            var affected = await _context.SaveChangesAsync();
            return affected;

        }
    }
}
