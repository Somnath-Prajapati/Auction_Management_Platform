using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AuctionManagementSystem.Application.Exceptions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.DepositlimitCalculation
{
    public class GetUserDepositLimitsQueryHandler : IRequestHandler<GetUserDepositLimitsQuery, UserDepositLimitDto>
    {
        private readonly IUserDepositRepository _userRepository;

        public GetUserDepositLimitsQueryHandler(IUserDepositRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDepositLimitDto> Handle(GetUserDepositLimitsQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserDepositLimitsAsync(request.UserId);

            if (result == null)
                throw new NotFoundException("User with id {request.id} does not found");

            return result;
        }
    }

}
