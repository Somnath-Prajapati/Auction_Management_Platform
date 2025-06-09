using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Roles;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Query.GetPermissionsByRoleId
{
    public record GetPermissionsByRoleIdQuery(int RoleId) : IRequest<RoleWithPermissionsDto>;
}
