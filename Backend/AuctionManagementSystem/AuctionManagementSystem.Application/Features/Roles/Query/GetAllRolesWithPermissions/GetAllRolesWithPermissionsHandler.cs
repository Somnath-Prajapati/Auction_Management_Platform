using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Roles;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Roles;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Query.GetAllRoles
{
    public class GetAllRolesWithPermissionsHandler : IRequestHandler<GetAllRolesWithPermissionsQuery, List<RoleWithPermissionsDto>>
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IRolePermissionsMatrixRepository _permissionsRepo;

        public GetAllRolesWithPermissionsHandler(
            IRoleRepository roleRepo,
            IRolePermissionsMatrixRepository permissionsRepo)
        {
            _roleRepo = roleRepo;
            _permissionsRepo = permissionsRepo;
        }

        public async Task<List<RoleWithPermissionsDto>> Handle(GetAllRolesWithPermissionsQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepo.GetAllRoles(); // Make sure this returns TblRole entities
            var permissionMatrix = await _permissionsRepo.GetAllPermissionsMatrixAsync(); // TblRolePermissionsMatrix

            var result = roles.Select(role =>
            {
                var matchedPermissions = permissionMatrix.FirstOrDefault(p => p.RoleId == role.RoleId);
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

            return result;
        }
    }
}
