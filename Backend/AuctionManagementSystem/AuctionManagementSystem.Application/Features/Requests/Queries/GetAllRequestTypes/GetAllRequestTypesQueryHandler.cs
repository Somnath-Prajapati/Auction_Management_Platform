using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Dtos.Requests;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestTypes
{
    public class GetAllRequestTypesQueryHandler : IRequestHandler<GetAllRequestTypesQuery, List<RequestTypeDto>>
    {
        private readonly IRequestRepository _repository;
        public GetAllRequestTypesQueryHandler(IRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<RequestTypeDto>> Handle(GetAllRequestTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllRequestTypesSPAsync();
        }
    }
} 