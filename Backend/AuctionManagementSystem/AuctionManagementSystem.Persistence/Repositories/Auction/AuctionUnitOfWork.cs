
using AuctionManagementSystem.Application.Contracts;

using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories;

namespace AuctionManagementSystem.Infrastructure.UoW
{
    public class AuctionUnitOfWork : IAuctionUnitOfWork
    {
        private readonly AuctionManagementDbContext _context;
        private IAuctionRepository _auctionRepository;

        public AuctionUnitOfWork(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public IAuctionRepository AuctionRepository =>
            _auctionRepository ??= new AuctionRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}