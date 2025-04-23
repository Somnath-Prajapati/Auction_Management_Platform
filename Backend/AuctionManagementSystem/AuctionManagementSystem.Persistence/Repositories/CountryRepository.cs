using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories
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
