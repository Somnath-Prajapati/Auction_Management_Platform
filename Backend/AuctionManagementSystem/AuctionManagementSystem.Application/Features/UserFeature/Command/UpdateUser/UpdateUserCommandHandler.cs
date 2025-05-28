using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IFileService fileService,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileService = fileService;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            var oldUserSnapshot = user.Clone(); // Shallow copy for audit trail
                                               
            var existingProfileImage = user.ProfileImage;
            var existingPersonalIdImage = user.PersonalIdImage;

            var existingUser = await _userRepository.GetByEmailOrMobileForUpdateAsync(request.Dto.Email, request.Dto.MobileNumber, request.Id);
            var goverementIdExists = await _userRepository.GetByPersonalIdNumberForUpdateAsync(request.Dto.PersonalIdNumber, request.Id);

            if (existingUser != null || goverementIdExists != null)
                throw new BadRequestException("A user with the same email, mobile number, or Government ID number already exists.");

            _mapper.Map(request.Dto, user);

            if (request.Dto.ProfileImage != null)
            {
                if (!string.IsNullOrEmpty(existingProfileImage))
                    await _fileService.DeleteFileAsync(existingProfileImage);

                user.ProfileImage = await _fileService.SaveFileAsync(request.Dto.ProfileImage, "");
            }
            else
            {
                user.ProfileImage = existingProfileImage;
            }

            if (request.Dto.PersonalIdImage != null)
            {
                if (!string.IsNullOrEmpty(existingPersonalIdImage))
                    await _fileService.DeleteFileAsync(existingPersonalIdImage);

                user.PersonalIdImage = await _fileService.SaveFileAsync(request.Dto.PersonalIdImage, "");
            }
            else
            {
                user.PersonalIdImage = existingPersonalIdImage;
            }

            user.LastOnline = DateTime.UtcNow;
            user.UpdatedBy = request.userId;
            user.UpdatedDate = DateTime.UtcNow;

            try
            {
                var result = await _userRepository.UpdateUserAsync(user);

                // Log audit trail for update
                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "User",
                    changeType: "Update",
                    recordId: user.UserId,
                    beforeChange: oldUserSnapshot,
                    afterChange: user
                );

                Console.WriteLine($"User Updated By: {_currentUser.Username} ({_currentUser.UserId}) with Role: {_currentUser.RoleName}");

                return result;
            }
            catch
            {
                throw new DatabaseException("Error updating user in the database.");
            }
        }
    }
}
