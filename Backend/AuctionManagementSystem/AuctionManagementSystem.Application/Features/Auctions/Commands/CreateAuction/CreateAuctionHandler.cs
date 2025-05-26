using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AuctionManagementSystem.Domain.Entities.Auction;
using MediatR;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction
{
    public class CreateAuctionHandler : IRequestHandler<CreateAuctionCommand, int>
    {
        private readonly IAuctionUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuctionJobScheduler _jobScheduler;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public CreateAuctionHandler(
            IAuctionUnitOfWork unitOfWork,
            IMapper mapper,
            IAuctionJobScheduler jobScheduler,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobScheduler = jobScheduler;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = _mapper.Map<TblAuction>(request);
            auction.CreatedBy = _currentUser.UserId.ToString();


            await _unitOfWork.AuctionRepository.AddAsync(auction);
            await _unitOfWork.SaveAsync();


            // Log the creation in the audit trail
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "Auction",
                changeType: "New",
                recordId: auction.AuctionId,
                beforeChange: null,
                afterChange: auction
            );

            Console.WriteLine($"Auction Created By: {_currentUser.Username} ({_currentUser.UserId}) with Role: {_currentUser.RoleName}");

            return auction.AuctionId;
        }
    }
}
