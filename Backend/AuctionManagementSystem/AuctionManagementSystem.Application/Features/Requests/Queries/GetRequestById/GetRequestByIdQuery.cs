using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Request;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestById
{
    public class GetRequestByIdQuery : IRequest<TblRequest>
    {
        public int RequestId { get; set; }

        public GetRequestByIdQuery(int requestId)
        {
            RequestId = requestId;
        }
    }
}