using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Application.Contracts.User
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
        Task<TblUser?> GetByPersonalIdNumberAsync(string personalIdNumber);
        Task<TblUser?> GetByPersonalIdNumberForUpdateAsync(string personalIdNumber, int excludeUserId);
        Task<TblUser?> GetByEmailOrMobileForUpdateAsync(string email, string mobileNumber, int excludeUserId);

    }
}
