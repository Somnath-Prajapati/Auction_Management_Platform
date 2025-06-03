using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.DepositlimitCalculation
{
    public class GetUserDepositLimitsQuery : IRequest<UserDepositLimitDto>
    {
        public int UserId { get; set; }

        public GetUserDepositLimitsQuery(int userId)
        {
            UserId = userId;
        }
    }

}
