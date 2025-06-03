using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Roles;
using AuctionManagementSystem.Application.Dtos.Roles;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.Roles
{
    public class RolePermissionsMatrixRepository : IRolePermissionsMatrixRepository
    {
        private readonly AuctionManagementDbContext _context;

        public RolePermissionsMatrixRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public async  Task<List<RolePermissionsMatrixDto>> GetAllPermissionsMatrixAsync()
        {
            return await _context.TblRolePermissionsMatrices
                .Select(r => new RolePermissionsMatrixDto
                {
                    RoleId = r.RoleId,
                    SuperAdmin = r.SuperAdmin,
                    AccessAdminPanel = r.AccessAdminPanel,
                    ManageAuctions = r.ManageAuctions,
                    ManageAssets = r.ManageAssets,
                    ManageTransactions = r.ManageTransactions,
                    ManageCategories = r.ManageCategories,
                    ManageRoles = r.ManageRoles,
                    ManageUsers = r.ManageUsers,
                    ViewReports = r.ViewReports,
                    ExportReports = r.ExportReports,
                    ManageRequests = r.ManageRequests,
                    ViewAuditTrail = r.ViewAuditTrail,
                    ChangeCommission = r.ChangeCommission
                }).ToListAsync();
        }
    }
}
