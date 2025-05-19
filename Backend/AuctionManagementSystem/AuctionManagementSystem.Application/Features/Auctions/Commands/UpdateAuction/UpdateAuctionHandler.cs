using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.Bids;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction
{
    public class UpdateAuctionHandler : IRequestHandler<UpdateAuctionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuctionJobScheduler _jobScheduler;

        public UpdateAuctionHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuctionJobScheduler jobScheduler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jobScheduler = jobScheduler;
        }

        public async Task<bool> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(request.AuctionId);
            if (auction == null)
            {
                return false;      
            }
            _mapper.Map(request, auction);
            _unitOfWork.AuctionRepository.Update(auction);
            await _unitOfWork.SaveAsync();
            _jobScheduler.ScheduleAuctionClosing(auction.AuctionId, auction.EndDateTime);
            return true;
        }
    }
}