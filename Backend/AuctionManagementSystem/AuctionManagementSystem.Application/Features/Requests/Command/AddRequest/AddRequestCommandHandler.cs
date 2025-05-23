using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Requests;
using AuctionManagementSystem.Domain.Entities.Request;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Requests.Command.AddRequest
{
    public class AddRequestCommandHandler : IRequestHandler<AddRequestCommand, CreateRequestDto>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public AddRequestCommandHandler(
            IRequestRepository requestRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<CreateRequestDto> Handle(AddRequestCommand request, CancellationToken cancellationToken)
        {
            request.RequestNumber = await _requestRepository.GenerateRequestNumberAsync();

            var user = await _userRepository.GetUserById(request.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {request.UserId} not found.");

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
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _requestRepository.AddRequest(newRequest);

            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Request",
                changeType: "New",
                recordId: newRequest.RequestId,
                beforeChange: null,
                afterChange: JsonConvert.SerializeObject(newRequest, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })
                );


            return _mapper.Map<CreateRequestDto>(newRequest);
        }
    }
}
