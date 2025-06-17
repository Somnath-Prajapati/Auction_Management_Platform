using AuctionManagementSystem.Application.Contracts.AuditTrail; // <-- Add this
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Notification;
using AuctionManagementSystem.Application.Contracts.Transactions; // <-- For serializing afterChange if needed
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Notification;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Domain.model;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using AuctionManagementSystem.Domain.model;
using AuctionManagementSystem.Application.Contracts.Transactions;
using Microsoft.AspNetCore.Authorization; // <-- For serializing afterChange if needed

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IAuditTrailService _auditTrailService; // <-- Add this
        private readonly ICurrentUserService _currentUser; // <-- Add this
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationBroadcaster _notificationBroadcaster;
        private readonly IUserDepositRepository _userDepositRepository; // <-- Add this

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IFileService fileService,
            IMapper mapper,
            ILoggedInUserService loggedInUserService,
            INotificationRepository notificationRepository,
            INotificationBroadcaster notificationBroadcaster,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser,
            IUserDepositRepository userDepositRepository) // <-- Inject this
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _mapper = mapper;
            _loggedInUserService = loggedInUserService;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
            _notificationRepository = notificationRepository;
            _notificationBroadcaster = notificationBroadcaster;
            _userDepositRepository = userDepositRepository; // <-- Initialize this
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
            user.AvailableLimit = user.TotalLimit ?? 0m;

            var userId = await _userRepository.AddUserAsync(user);

            var userNotification = new TblNotification
            {
                NotificationId = Guid.NewGuid(),
                UserId = null,
                Title = "New User Added",
                Message = $"New User Added {user.Name}",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                IsRead = false
            };


            await _notificationRepository.CreateAsync(userNotification);


            var notificationDto = new NotificationDto
            {
                UserId = userId,
                Title = userNotification.Title,
                Message = userNotification.Message,
                CreatedAt = userNotification.CreatedAt,
                ExpiresAt = userNotification.ExpiresAt
            };

            await _notificationBroadcaster.NotifyByRole(notificationDto, "Admin");


            var userDeposit = new TblUserDeposit
            {
                UserId = userId,
                DepositAmount = user.Deposit ?? 0m, // Explicitly handle nullable decimal
                ModifiedAt = user.CreatedDate ?? DateTime.UtcNow,
                CreatedBy = request.userid,
                CreatedDate = user.CreatedDate ?? DateTime.UtcNow,
                UpdatedBy = request.userid,
                UpdatedDate = user.UpdatedDate ?? DateTime.UtcNow,
                IsDeleted = false
            };

            await _userDepositRepository.AddAsync(userDeposit);

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
