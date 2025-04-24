using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.User
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AuctionManagementDbContext _context;
        public CountryRepository(AuctionManagementDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<TblCountry>> GetAllCountries()
        {
            return await _context.TblCountries.ToListAsync();

        }

        public async Task<TblCountry> GetCountryById(int id)
        {
            return await _context.TblCountries.FindAsync(id);
        }
    }
}
