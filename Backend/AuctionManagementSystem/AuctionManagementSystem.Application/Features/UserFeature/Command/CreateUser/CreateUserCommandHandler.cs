using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Application.Exceptions;  // Add this namespace for custom exceptions
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IFileService fileService, IMapper mapper)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailOrMobileAsync(request.UserDto.Email, request.UserDto.MobileNumber);
            if (existingUser != null)
            {
                // Throw a BadRequestException if user already exists
                throw new BadRequestException("A user with the same email or mobile number already exists.");
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

            user.RegistrationDate = DateTime.UtcNow;
            user.LastOnline = DateTime.UtcNow;
            user.CreatedBy = "Admin";
            user.CreatedDate = DateTime.UtcNow;
            user.IsDeleted = false;

            // Wrap the user addition in a try-catch to throw a more specific exception for database issues
            try
            {
                return await _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                // Log the exception and throw a more specific exception, for example a DatabaseException
                throw new DatabaseException("An error occurred while adding the user to the database.");
            }
        }
    }
}
