using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Assets.Auth;
using AuctionManagementSystem.Application.Dtos.Roles;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AuthFeatures.Command.VerifyOtp
{
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, AuthResponseDto>
    {
        private readonly IUnitOfWorkAuth _unitOfWork;
        private readonly IJwtService _jwtService;
        //new added
        private readonly IRoleRepository _roleRepository;

        public VerifyOtpCommandHandler(IUnitOfWorkAuth unitOfWork, IJwtService jwtService, IRoleRepository roleRepository)
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

            //otp.IsUsed = true;
            _unitOfWork.OtpRepository.Delete(otp);
            user.LastOnline = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();

            // Get user role
            var role = await _unitOfWork.RoleRepository.GetRoleByIdWithPermissionsAsync(user.RoleId);
            var roleName = role?.RoleName ?? "User";

            var token = _jwtService.GenerateToken(user, roleName);


            var permissions = role.TblRolePermissionsMatrices.FirstOrDefault();

            var roleDto = new RoleWithPermissionsDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                IsSeller = role.IsSeller,

                SuperAdmin = permissions?.SuperAdmin ?? false,
                AccessAdminPanel = permissions?.AccessAdminPanel ?? false,
                ManageAuctions = permissions?.ManageAuctions ?? false,
                ManageAssets = permissions?.ManageAssets ?? false,
                ManageTransactions = permissions?.ManageTransactions ?? false,
                ManageCategories = permissions?.ManageCategories ?? false,
                ManageRoles = permissions?.ManageRoles ?? false,
                ManageUsers = permissions?.ManageUsers ?? false,
                ViewReports = permissions?.ViewReports ?? false,
                ExportReports = permissions?.ExportReports ?? false,
                ManageRequests = permissions?.ManageRequests ?? false,
                ViewAuditTrail = permissions?.ViewAuditTrail ?? false,
                ChangeCommission = permissions?.ChangeCommission ?? false
            };


            return new AuthResponseDto
            {
                Token = token,
                //Role = roleName,
                Email = user.Email,
                Name = user.Name,
                Role = roleDto
            };
        }
    }


}
