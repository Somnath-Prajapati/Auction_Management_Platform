using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Notification;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Notifications.Command.MarkAsRead
{
    public class MarkAsReadCommandHandler : IRequestHandler<MarkAsReadCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            await _notificationRepository.MarkAsReadAsync(request.NotificationId);
            return Unit.Value;
        }
    }
}
