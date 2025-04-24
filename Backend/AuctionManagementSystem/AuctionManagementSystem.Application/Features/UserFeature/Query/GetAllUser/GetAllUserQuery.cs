using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllUser
{
    public record GetAllUsersQuery() : IRequest<List<GetUserDto>>;
}
