using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AuctionManagementDbContext _context;

        public AuctionRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TblAuction>> GetAllAsync()
        {
            return await _context.TblAuctions
               .Include(a => a.Category)
               .Include(a => a.Status)
               .ToListAsync();
        }

        public async Task<TblAuction> GetByIdAsync(int id)
        {
            return await _context.TblAuctions.FindAsync(id);
        }

        public async Task AddAsync(TblAuction auction)
        {
            await _context.TblAuctions.AddAsync(auction);
        }

        public void Update(TblAuction auction)
        {
            _context.TblAuctions.Update(auction);
        }

        public void Delete(TblAuction auction)
        {
            _context.TblAuctions.Remove(auction);
        }
    }
}
