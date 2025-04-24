using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.UserDtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllUser
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GetUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

            var userDtos = users.Select(user =>
            {
                var dto = _mapper.Map<GetUserDto>(user);
                dto.ProfileImageUrl = user.ProfileImage != null ? $"{baseUrl}/{user.ProfileImage}" : null;
                dto.PersonalIdImageUrl = user.PersonalIdImage != null ? $"{baseUrl}/{user.PersonalIdImage}" : null;
                return dto;
            }).ToList();

            return userDtos;
        }


    }
}
