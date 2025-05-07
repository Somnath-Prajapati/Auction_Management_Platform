using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteUser
{
    public record DeleteUserCommand(int Id, string userId) : IRequest<bool>;
}
