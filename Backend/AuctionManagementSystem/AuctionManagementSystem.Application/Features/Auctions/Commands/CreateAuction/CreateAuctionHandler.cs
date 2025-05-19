using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AuctionManagementSystem.Domain.Entities;
using MediatR;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Domain.Entities.Auction;
using AuctionManagementSystem.Application.Contracts.Bids;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction
{
    public class CreateAuctionHandler : IRequestHandler<CreateAuctionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuctionJobScheduler _jobScheduler;

        public CreateAuctionHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuctionJobScheduler jobScheduler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobScheduler = jobScheduler;
        }

        public async Task<int> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {


            // Mapping from CreateAuctionCommand to TblAuction
            var auction = _mapper.Map<TblAuction>(request);
            auction.CreatedBy = request.UserId;

            // Add the auction to the database
            await _unitOfWork.AuctionRepository.AddAsync(auction);

            // Save changes
            await _unitOfWork.SaveAsync();

            // Return the AuctionId
            _jobScheduler.ScheduleAuctionClosing(auction.AuctionId, auction.EndDateTime);
            return auction.AuctionId;
        }
    }
}
