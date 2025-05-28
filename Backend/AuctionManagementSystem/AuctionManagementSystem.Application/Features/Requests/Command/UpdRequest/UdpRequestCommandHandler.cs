using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.Request;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Requests.Command.UpdRequest
{
    public class UdpRequestCommandHandler : IRequestHandler<UdpRequestCommand, TblRequest>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public UdpRequestCommandHandler(
            IRequestRepository requestRepository,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _requestRepository = requestRepository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<TblRequest> Handle(UdpRequestCommand request, CancellationToken cancellationToken)
        {
            var existingRequest = await _requestRepository.GetRequestByIdQuery(request.RequestId);
            if (existingRequest == null)
                return null;

            // Serialize pre-update state
            var beforeUpdate = JsonConvert.SerializeObject(existingRequest);

            // Update entity
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
            existingRequest.UpdatedOn = DateTime.UtcNow;

            await _requestRepository.UpdateRequest(existingRequest);

            // Serialize post-update state and log to audit
            var afterUpdate = JsonConvert.SerializeObject(existingRequest);
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Request",
                changeType: "Update",
                recordId: existingRequest.RequestId,
                beforeChange: beforeUpdate,
                afterChange: afterUpdate
            );

            return existingRequest;
        }
    }
}
