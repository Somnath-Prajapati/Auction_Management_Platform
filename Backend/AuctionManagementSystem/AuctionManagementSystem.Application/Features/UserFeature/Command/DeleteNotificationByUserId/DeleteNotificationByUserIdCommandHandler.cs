using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Notification;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteNotificationByUserId
{
    public class DeleteNotificationByUserIdCommandHandler : IRequestHandler<DeleteNotificationByUserIdCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;

        public DeleteNotificationByUserIdCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(DeleteNotificationByUserIdCommand request, CancellationToken cancellationToken)
        {
            await _notificationRepository.DeleteNotififcationByUserId(request.UserId);
            return Unit.Value;
        }
    }
}
