using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.UpdateUser
{
    public record UpdateUserCommand(int Id, UserDto Dto) : IRequest<int>;
}

