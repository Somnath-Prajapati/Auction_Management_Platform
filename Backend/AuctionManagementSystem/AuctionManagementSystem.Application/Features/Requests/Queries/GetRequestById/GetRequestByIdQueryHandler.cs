using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestById;
using AuctionManagementSystem.Domain.Entities.Request;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetRequestById
{
    public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, TblRequest>
    {
        private readonly IRequestRepository _requestRepository;

        public GetRequestByIdQueryHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<TblRequest> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _requestRepository.GetRequestByIdQuery(request.RequestId);
        }
    }
}
