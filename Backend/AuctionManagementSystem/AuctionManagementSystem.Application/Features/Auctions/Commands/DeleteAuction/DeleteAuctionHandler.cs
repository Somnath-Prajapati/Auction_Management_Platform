using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Domain.Entities.Auction;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.DeleteAuction
{
    public class DeleteAuctionHandler : IRequestHandler<DeleteAuctionCommand, bool>
    {
        private readonly IAuctionUnitOfWork _unitOfWork;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;
        private readonly IAuctionJobScheduler _jobScheduler;

        public DeleteAuctionHandler(IAuctionUnitOfWork unitOfWork, IAuditTrailService auditTrailService, ICurrentUserService currentUser, IAuctionJobScheduler jobScheduler)
        {
            _unitOfWork = unitOfWork;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
            _jobScheduler = jobScheduler;
        }

        public async Task<bool> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(request.AuctionId);
            if (auction == null || auction.IsDeleted)
                return false;

            var oldAuction = JsonConvert.DeserializeObject<TblAuction>(JsonConvert.SerializeObject(auction));

            if (!string.IsNullOrEmpty(auction.HangfireJobId))
            {
                _jobScheduler.CancelScheduledAuctionClosing(auction.HangfireJobId);
            }

            _unitOfWork.AuctionRepository.Delete(auction);
            await _unitOfWork.SaveAsync();

            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Auction",
                changeType: "Delete",
                recordId: auction.AuctionId,
                beforeChange: oldAuction,
                afterChange: null // Nothing after deletion
            );

            Console.WriteLine($"CurrentUser: {_currentUser.UserId}, {_currentUser.Username}");
            Console.WriteLine($"Audit RoleName: {_currentUser.RoleName}");

            return true;
        }
    }
}
