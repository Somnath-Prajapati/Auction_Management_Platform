using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Dtos.Requests;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestStatuses
{
    public class GetAllRequestStatusesQueryHandler : IRequestHandler<GetAllRequestStatusesQuery, List<RequestStatusDto>>
    {
        private readonly IRequestRepository _repository;
        public GetAllRequestStatusesQueryHandler(IRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<RequestStatusDto>> Handle(GetAllRequestStatusesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllDistinctRequestStatusesSPAsync();
        }
    }
} 