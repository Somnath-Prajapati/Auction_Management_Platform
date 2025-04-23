using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Application.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<TblUser>> GetAllUsersAsync();
        Task<TblUser> GetUserById(int id);
        Task<TblUser> CreateUserAsync(TblUser tblUser);
        Task<bool> UpdateUserAsync(int id, TblUser tblUser);
        Task<bool> DeleteUserAsync(int id);

    }
}
