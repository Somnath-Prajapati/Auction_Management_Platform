using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Roles;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Application.Contracts.User
{
    public interface IRoleRepository
    {
        Task<IEnumerable<TblRole>> GetAllAsync();
        Task<TblRole> GetRoleByIdAsync(int roleId);
        Task<int?> GetRoleIdByName(string roleName);
        Task<List<RoleWithPermissionsDto>> GetAllRoles();
    }

}
