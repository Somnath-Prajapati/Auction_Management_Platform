using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
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
            if (user == null) throw new KeyNotFoundException("User not found");

            _mapper.Map(request.Dto, user);

            if (request.Dto.ProfileImage != null)
            {
                user.ProfileImage = await _fileService.SaveFileAsync(request.Dto.ProfileImage, "");
            }

            if (request.Dto.PersonalIdImage != null)
            {
                user.PersonalIdImage = await _fileService.SaveFileAsync(request.Dto.PersonalIdImage, "");
            }
            user.RegistrationDate = DateTime.UtcNow;
            user.LastOnline = DateTime.UtcNow;

            return await _userRepository.UpdateUserAsync(user);
        }
    }

}
