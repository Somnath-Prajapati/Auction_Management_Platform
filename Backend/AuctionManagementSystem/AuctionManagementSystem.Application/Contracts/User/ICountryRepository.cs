using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Application.Contracts.User
{
    public interface ICountryRepository
    {
        Task<IEnumerable<TblCountry>> GetAllCountries();
        Task<TblCountry> GetCountryById(int id);
    }
}
