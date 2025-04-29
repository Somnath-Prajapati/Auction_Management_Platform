using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Domain.Entities.Request;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Command.UpdRequest
{
    public class UdpRequestCommandHandler : IRequestHandler<UdpRequestCommand, TblRequest>
    {
        private readonly IRequestRepository _requestRepository;

        public UdpRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<TblRequest> Handle(UdpRequestCommand request, CancellationToken cancellationToken)
        {
            var existingRequest = await _requestRepository.GetRequestByIdQuery(request.RequestId);
            if (existingRequest == null)
                return null;

            existingRequest.RequestNumber = request.RequestNumber;
            existingRequest.UserId = request.UserId;
            existingRequest.Username = request.Username;
            existingRequest.MobileNumber = request.MobileNumber;
            existingRequest.Email = request.Email;
            existingRequest.RequestTypeId = request.RequestTypeId;
            existingRequest.AssetId = request.AssetId;
            existingRequest.TransactionId = request.TransactionId;
            existingRequest.RequestDateTime = request.RequestDateTime;
            existingRequest.RequestStatusId = request.RequestStatusId;
            existingRequest.CustomerNote = request.CustomerNote;
            existingRequest.AdminNote = request.AdminNote;
            existingRequest.CreatedByAdmin = request.CreatedByAdmin;
            existingRequest.UpdatedOn = System.DateTime.Now;

            await _requestRepository.UpdateRequest(existingRequest);
            return existingRequest;
        }
    }
}