using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Roles;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Command.UpdateRoles
{
    public record UpdateRoleWithPermissionsCommand(RoleWithPermissionsDto RoleDto) : IRequest<bool>;
}
