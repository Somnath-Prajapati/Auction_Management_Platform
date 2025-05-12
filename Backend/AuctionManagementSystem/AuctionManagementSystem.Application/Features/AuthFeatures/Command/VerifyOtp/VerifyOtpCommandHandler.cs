using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Auth;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AuthFeatures.Command.VerifyOtp
{
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, AuthResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        //new added
        private readonly IRoleRepository _roleRepository;

        public VerifyOtpCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
        }

        public async Task<AuthResponseDto> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.dto.Email);
            if (user == null) return null;

            var otp = await _unitOfWork.OtpRepository.GetValidOtpAsync(user.UserId, request.dto.Code);
            if (otp == null) return null;

            otp.IsUsed = true;
            await _unitOfWork.SaveChangesAsync();

            // Get user role
            var role = await _unitOfWork.RoleRepository.GetRoleByIdAsync(user.RoleId);
            var roleName = role?.RoleName ?? "User";

            var token = _jwtService.GenerateToken(user, roleName);

            return new AuthResponseDto
            {
                Token = token,
                Role = roleName,
                Email = user.Email,
                Name = user.Name
            };
        }
    }


}
