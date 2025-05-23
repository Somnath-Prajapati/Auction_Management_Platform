using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Request;
using AuctionManagementSystem.Application.Contracts.User;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Requests.Command.DelRequest
{
    public class DelRequestCommandHandler : IRequestHandler<DelRequestCommand, bool>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public DelRequestCommandHandler(
            IRequestRepository requestRepository,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _requestRepository = requestRepository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(DelRequestCommand request, CancellationToken cancellationToken)
        {
            var existingRequest = await _requestRepository.GetRequestByIdQuery(request.RequestId);

            if (existingRequest == null || existingRequest.IsDeleted)
                return false;

            // Capture before state
            var beforeState = JsonConvert.SerializeObject(existingRequest);

            // Perform soft delete
            existingRequest.IsDeleted = true;
            existingRequest.DeletedBy = request.DeletedBy;
            existingRequest.DeletedDate = DateTime.UtcNow;

            await _requestRepository.UpdateRequest(existingRequest);

            // Capture after state and log audit
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Request",
                changeType: "Delete",
                recordId: existingRequest.RequestId,
                beforeChange: beforeState,
                afterChange: JsonConvert.SerializeObject(existingRequest)
            );

            return true;
        }
    }
}
