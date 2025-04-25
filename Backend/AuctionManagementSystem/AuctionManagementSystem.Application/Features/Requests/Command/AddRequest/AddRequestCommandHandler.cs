using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Domain.Entities.Request;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Command.AddRequest
{
    public class AddRequestCommandHandler : IRequestHandler<AddRequestCommand, TblRequest>
    {
        private readonly IRequestRepository _requestRepository;

        public AddRequestCommandHandler(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<TblRequest> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
            var newRequest = new TblRequest
            {
                RequestNumber = request.RequestNumber,
                UserId = request.UserId,
                Username = request.Username,
                MobileNumber = request.MobileNumber,
                Email = request.Email,
                RequestTypeId = request.RequestTypeId,
                AssetId = request.AssetId,
                TransactionId = request.TransactionId,
                RequestDateTime = request.RequestDateTime,
                RequestStatusId = request.RequestStatusId,
                CustomerNote = request.CustomerNote,
                AdminNote = request.AdminNote,
                CreatedByAdmin = request.CreatedByAdmin,
                CreatedOn = System.DateTime.Now,
                UpdatedOn = System.DateTime.Now
            };

            await _requestRepository.AddRequest(newRequest);
            return newRequest;
        }
    }
}
