using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Notifications.Command.MarkAsRead
{
    public record MarkAsReadCommand(Guid NotificationId) : IRequest<Unit>;
}
