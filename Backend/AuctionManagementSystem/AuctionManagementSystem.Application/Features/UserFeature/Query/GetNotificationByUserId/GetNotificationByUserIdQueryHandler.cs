using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Notification;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Notification;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Query.GetNotificationByUserId
{
    public class GetNotificationByUserIdQueryHandler : IRequestHandler<GetNotificationByUserIdQuery, IEnumerable<NotificationDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetNotificationByUserIdQueryHandler(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NotificationDto>> Handle(GetNotificationByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetNotificationByUserId(request.UserId, request.langCode);

            if (result == null || !result.Any())
            {
                throw new NotFoundException("No notifications available");
            }

            var notifications = _mapper.Map<IEnumerable<NotificationDto>>(result);
            return notifications;
        }

    }
}
