using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities;

namespace AuctionManagementSystem.Application.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<TblUser>> GetAllUsersAsync();
        Task<TblUser> GetUserById(int id);
        Task<int> AddUserAsync(TblUser tblUser);
        Task<int> UpdateUserAsync(TblUser tblUser);
        Task<bool> DeleteUserAsync(TblUser user);
        Task<int> GenerateNextUidAsync(int startFrom = 1003);
        Task<TblUser?> GetByEmailOrMobileAsync(string email, string mobileNumber);

    }
}
