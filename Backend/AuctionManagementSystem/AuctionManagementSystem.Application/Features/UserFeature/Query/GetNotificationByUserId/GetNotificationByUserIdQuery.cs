using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Notification;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Query.GetNotificationByUserId
{
    public record GetNotificationByUserIdQuery(int UserId) : IRequest<IEnumerable<NotificationDto>>;
}
