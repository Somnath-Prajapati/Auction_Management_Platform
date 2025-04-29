using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Command.DelRequest
{
    public class DelRequestCommandHandler : IRequestHandler<DelRequestCommand, bool>
    {
        private readonly IRequestRepository _requestRepository;

        public DelRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<bool> Handle(DelRequestCommand request, CancellationToken cancellationToken)
        {
            var existingRequest = await _requestRepository.GetRequestByIdQuery(request.RequestId);
            if (existingRequest == null)
                return false;

            await _requestRepository.DelRequest(existingRequest);
            return true;
        }
    }
}