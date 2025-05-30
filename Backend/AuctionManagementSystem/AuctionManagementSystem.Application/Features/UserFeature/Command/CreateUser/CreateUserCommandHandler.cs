using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.AuditTrail; // <-- Add this
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json; // <-- For serializing afterChange if needed

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IAuditTrailService _auditTrailService; 
        private readonly ICurrentUserService _currentUser; 

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IFileService fileService,
            IMapper mapper,
            ILoggedInUserService loggedInUserService,
            IAuditTrailService auditTrailService, 
            ICurrentUserService currentUser)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _mapper = mapper;
            _loggedInUserService = loggedInUserService;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailOrMobileAsync(request.UserDto.Email, request.UserDto.MobileNumber);
            var existingPersonalIdUser = await _userRepository.GetByPersonalIdNumberAsync(request.UserDto.PersonalIdNumber);

            if (existingUser != null || existingPersonalIdUser != null)
            {
                throw new BadRequestException("A user with the same email, mobile number, or Goverment ID number already exists.");
            }

            var user = _mapper.Map<TblUser>(request.UserDto);
            user.Uid = await _userRepository.GenerateNextUidAsync(startFrom: 1003);

            if (request.UserDto.ProfileImage != null)
            {
                user.ProfileImage = await _fileService.SaveFileAsync(request.UserDto.ProfileImage, "ProfileImages");
            }

            if (request.UserDto.PersonalIdImage != null)
            {
                user.PersonalIdImage = await _fileService.SaveFileAsync(request.UserDto.PersonalIdImage, "PersonalIdImages");
            }

            user.CreatedBy = request.userid;
            user.RegistrationDate = DateTime.UtcNow;
            user.LastOnline = DateTime.UtcNow;
            user.CreatedDate = DateTime.UtcNow;
            user.IsDeleted = false;
            var userId = await _userRepository.AddUserAsync(user);
            try
            {
                Console.WriteLine($"[DEBUG] UserId: {_currentUser.UserId}, Username: {_currentUser.Username}, Role: {_currentUser.RoleName}");
                var afterChangeJson = JsonConvert.SerializeObject(user, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                // Audit Trail Logging
                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "User",
                    changeType: "New",
                    recordId: userId,
                    beforeChange: null,
                    afterChange: afterChangeJson
                );

                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audit Trail Logging Failed: " + ex.Message);
                throw new DatabaseException("An error occurred while adding the user to the database.");
            }
        }
    }
}
