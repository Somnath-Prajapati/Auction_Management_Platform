using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Roles;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Query.GetPermissionsByRoleId
{
    public class GetPermissionsByRoleIdHandler : IRequestHandler<GetPermissionsByRoleIdQuery, RoleWithPermissionsDto>
    {
        private readonly IRoleRepository _roleRepo;

        public GetPermissionsByRoleIdHandler(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<RoleWithPermissionsDto> Handle(GetPermissionsByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepo.GetRoleByIdWithPermissionsAsync(request.RoleId);
            if (role == null)
                return null;

            var p = role.TblRolePermissionsMatrices.FirstOrDefault();

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
        }
    }
}
