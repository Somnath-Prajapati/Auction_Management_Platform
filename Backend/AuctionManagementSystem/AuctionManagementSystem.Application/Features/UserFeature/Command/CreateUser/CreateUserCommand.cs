using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser
{
    public record CreateUserCommand(UserDto UserDto) : IRequest<int>;
}
