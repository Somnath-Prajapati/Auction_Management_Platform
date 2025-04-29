using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Command.DelRequest
{
    public class DelRequestCommand : IRequest<bool>
    {
        public int RequestId { get; set; }

        public DelRequestCommand(int requestId)
        {
            RequestId = requestId;
        }
    }
}