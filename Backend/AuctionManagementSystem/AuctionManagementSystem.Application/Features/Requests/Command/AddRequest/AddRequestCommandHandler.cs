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
                RequestNumber = GenerateRequestNumber(),
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

                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            await _requestRepository.AddRequest(newRequest);
            return newRequest;
        }

        private string GenerateRequestNumber()
        {
            var today = DateTime.Now;
            var datePart = today.ToString("yyyyMMdd"); // Example: 20250427
            var random = new Random();
            var randomNumber = random.Next(1, 1000).ToString("D3"); // Random 001-999
            return $"REQ-{datePart}-{randomNumber}";
        }

    }
}
