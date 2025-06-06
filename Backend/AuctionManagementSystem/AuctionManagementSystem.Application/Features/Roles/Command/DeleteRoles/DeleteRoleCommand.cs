using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Command.DeleteRoles
{
    public record DeleteRoleCommand(int RoleId) : IRequest<bool>;

}
