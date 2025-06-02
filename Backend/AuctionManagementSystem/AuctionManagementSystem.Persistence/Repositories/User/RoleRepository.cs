using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Roles;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
using AuctionManagementSystem.Persistence.Repositories.Roles;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.User
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AuctionManagementDbContext _context;

        public RoleRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }


        //new added
        public async Task<int?> GetRoleIdByName(string roleName)
        {
            var role = await _context.TblRoles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);

            return role?.RoleId; // Return RoleId if found, else null
        }


        public async Task<IEnumerable<TblRole>> GetAllAsync()
        {
            return await _context.TblRoles.ToListAsync();
        }
        public async Task<TblRole> GetRoleByIdAsync(int roleId)
        {
            return await _context.TblRoles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        }
        public async Task<List<TblRole>> GetAllRoles()
        {
            var roles = await _context.TblRoles.ToListAsync(); // safe — EF only reads mapped columns

            var permissionsMatrix = await _context.TblRolePermissionsMatrices.ToListAsync();

            // Now do in-memory mapping
            var result = roles.Select(role =>
            {
                var matchedPermissions = permissionsMatrix.FirstOrDefault(p => p.RoleId == role.RoleId);
                var permissionsList = new List<string>();

                if (matchedPermissions != null)
                {
                    var props = matchedPermissions.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        if (prop.Name != "RoleId" && prop.PropertyType == typeof(int))
                        {
                            var value = (int)prop.GetValue(matchedPermissions)!;
                            if (value == 1)
                            {
                                permissionsList.Add(prop.Name);
                            }
                        }
                    }
                }

                return new RoleWithPermissionsDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    IsSeller = role.IsSeller,
                    //Permissions = permissionsList
                };
            }).ToList();




            return roles;
        }
    }
}