using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using AuctionManagementSystem.Application.Exceptions;  // Add this namespace for custom exceptions
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using AuctionManagementSystem.Application.Contracts.Auth;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserService _loggedInUserService;

        public CreateUserCommandHandler(IUserRepository userRepository, IFileService fileService, IMapper mapper, ILoggedInUserService loggedInUserService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _mapper = mapper;
            _loggedInUserService = loggedInUserService;
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

            try
            {
                return await _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("An error occurred while adding the user to the database.");
            }
        }
    }
}
