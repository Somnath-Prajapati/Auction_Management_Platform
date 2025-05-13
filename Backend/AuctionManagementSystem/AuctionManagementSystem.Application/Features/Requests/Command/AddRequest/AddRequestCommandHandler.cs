using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Requests;
using AuctionManagementSystem.Domain.Entities.Request;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Command.AddRequest
{
    //public class AddRequestCommandHandler : IRequestHandler<AddRequestCommand, TblRequest>
    public class AddRequestCommandHandler : IRequestHandler<AddRequestCommand, CreateRequestDto >
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //private readonly Impper

        public AddRequestCommandHandler(IRequestRepository requestRepository, IUserRepository userRepository, IMapper mapper)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public async Task<CreateRequestDto> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
            // Generate a new sequential request number
            request.RequestNumber = await _requestRepository.GenerateRequestNumberAsync();

            var user = await _userRepository.GetUserById(request.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

            // Create new request entity
            var newRequest = new TblRequest
            {
                RequestNumber = request.RequestNumber,
                UserId = request.UserId,
                Username = request.Username,  // ensure this is set in command
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
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            // Persist the new request
            await _requestRepository.AddRequest(newRequest);

            // Map entity to DTO and return
            var responseDto = _mapper.Map<CreateRequestDto>(newRequest);
        
            return responseDto;
        }

        //private string GenerateRequestNumber()
        //{
        //    var today = DateTime.Now;
        //    var datePart = today.ToString("yyyyMMdd"); // Example: 20250427
        //    var random = new Random();
        //    var randomNumber = random.Next(1, 1000).ToString("D3"); // Random 001-999
        //    return $"REQ-{datePart}-{randomNumber}";
        //}

    }
}
