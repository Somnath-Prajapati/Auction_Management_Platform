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

        public GetAllRolesWithPermissionsHandler(
            IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<List<RoleWithPermissionsDto>> Handle(GetAllRolesWithPermissionsQuery request, CancellationToken cancellationToken)
        {
            var rolesWithPermissions = await _roleRepo.GetAllRoles();
            return rolesWithPermissions;
        }
    }
    
}
