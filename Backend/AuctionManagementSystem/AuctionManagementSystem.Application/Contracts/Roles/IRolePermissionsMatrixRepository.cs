using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Roles;
using AuctionManagementSystem.Domain.Entities.Roles;

namespace AuctionManagementSystem.Application.Contracts.Roles
{
    public interface IRolePermissionsMatrixRepository
    {
        Task<List<RoleWithPermissionsDto>> GetAllPermissionsMatrixAsync();
    }

}
