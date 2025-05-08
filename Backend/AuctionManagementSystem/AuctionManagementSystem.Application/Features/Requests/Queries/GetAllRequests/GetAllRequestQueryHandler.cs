using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Dtos.Requests;
using AuctionManagementSystem.Domain.Entities.Request;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequests
{
    public class GetAllRequestQueryHandler : IRequestHandler<GetAllRequestQuery, IEnumerable<RequestDto>>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;

        public GetAllRequestQueryHandler(IRequestRepository requestRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequestDto>> Handle(GetAllRequestQuery request, CancellationToken cancellationToken)

        {
            var requests = await _requestRepository.GetAllRequestQuery();
            return _mapper.Map<IEnumerable<RequestDto>>(requests);
        }
    }
}