using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IFileService fileService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            var existingProfileImage = user.ProfileImage;
            var existingPersonalIdImage = user.PersonalIdImage;

            _mapper.Map(request.Dto, user);

            // Handle profile image update
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

            // Handle personal ID image update
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
            user.UpdatedBy = "Admin";
            user.UpdatedDate = DateTime.UtcNow;

            try
            {
                return await _userRepository.UpdateUserAsync(user);
            }
            catch
            {
                throw new DatabaseException("Error updating user in the database.");
            }
        }
    }
}
