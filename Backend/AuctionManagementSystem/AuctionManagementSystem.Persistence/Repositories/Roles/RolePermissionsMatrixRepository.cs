//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AuctionManagementSystem.Application.Contracts.Roles;
//using AuctionManagementSystem.Application.Dtos.Roles;
//using AuctionManagementSystem.Persistence.Context;
//using Microsoft.EntityFrameworkCore;

//namespace AuctionManagementSystem.Persistence.Repositories.Roles
//{
//    public class RolePermissionsMatrixRepository : IRolePermissionsMatrixRepository
//    {
//        private readonly AuctionManagementDbContext _context;

//        public RolePermissionsMatrixRepository(AuctionManagementDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<RolePermissionsMatrixDto>> GetAllPermissionsMatrixAsync()
//        {
//            return await _context.TblRolePermissionsMatrices
//                .Select(r => new RolePermissionsMatrixDto
//                {
//                    RoleId = r.RoleId,
//                    SuperAdmin = r.SuperAdmin ?? false,
//                    AccessAdminPanel = r.AccessAdminPanel ?? false,
//                    ManageAuctions = r.ManageAuctions ?? false,
//                    ManageAssets = r.ManageAssets ?? false,
//                    ManageTransactions = r.ManageTransactions ?? false,
//                    ManageCategories = r.ManageCategories ?? false,
//                    ManageRoles = r.ManageRoles ?? false,
//                    ManageUsers = r.ManageUsers ?? false,
//                    ViewReports = r.ViewReports ?? false,
//                    ExportReports = r.ExportReports ?? false,
//                    ManageRequests = r.ManageRequests ?? false,
//                    ViewAuditTrail = r.ViewAuditTrail ?? false,
//                    ChangeCommission = r.ChangeCommission ?? false
//                })
//                .ToListAsync();
//        }
//    }
//}
