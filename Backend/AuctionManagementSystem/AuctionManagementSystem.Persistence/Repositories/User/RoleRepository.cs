using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Roles;
using AuctionManagementSystem.Domain.Entities.Roles;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Persistence.Context;
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

        public async Task<int?> GetRoleIdByName(string roleName)
        {
            var role = await _context.TblRoles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);

            return role?.RoleId;
        }

        public async Task<IEnumerable<TblRole>> GetAllAsync()
        {
            return await _context.TblRoles.ToListAsync();
        }

        public async Task<TblRole> GetRoleByIdAsync(int roleId)
        {
            return await _context.TblRoles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        }

        public async Task<List<RoleWithPermissionsDto>> GetAllRoles()
        {
            var roles = await _context.TblRoles
                .Where(r => !r.IsDeleted)
                .Include(r => r.TblRolePermissionsMatrices) // Include the permissions list
                .ToListAsync();
            foreach (var role in roles)
            {
                var p = role.TblRolePermissionsMatrices.FirstOrDefault();

                Console.WriteLine($"Role: {role.RoleName} - SuperAdmin: {p?.SuperAdmin}");
            }

            var result = roles.Select(role =>
            {
                var p = role.TblRolePermissionsMatrices.FirstOrDefault(); // assume 1 matrix per role

                return new RoleWithPermissionsDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    IsSeller = role.IsSeller,

                    SuperAdmin = p?.SuperAdmin == true,
                    AccessAdminPanel = p?.AccessAdminPanel == true,
                    ManageAuctions = p?.ManageAuctions == true,
                    ManageAssets = p?.ManageAssets == true,
                    ManageTransactions = p?.ManageTransactions == true,
                    ManageCategories = p?.ManageCategories == true,
                    ManageRoles = p?.ManageRoles == true,
                    ManageUsers = p?.ManageUsers == true,
                    ViewReports = p?.ViewReports == true,
                    ExportReports = p?.ExportReports == true,
                    ManageRequests = p?.ManageRequests == true,
                    ViewAuditTrail = p?.ViewAuditTrail == true,
                    ChangeCommission = p?.ChangeCommission == true
                };
            }).ToList();

            return result;
        }
        public async Task<TblRole?> GetRoleByIdWithPermissionsAsync(int roleId)
        {
            return await _context.TblRoles
                .Where(r => !r.IsDeleted)
                .Include(r => r.TblRolePermissionsMatrices)
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }
        public async Task AddRoleAsync(TblRole role)
        {
            _context.TblRoles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task AddRolePermissionsAsync(TblRolePermissionsMatrix permissions)
        {
            _context.TblRolePermissionsMatrices.Add(permissions);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
