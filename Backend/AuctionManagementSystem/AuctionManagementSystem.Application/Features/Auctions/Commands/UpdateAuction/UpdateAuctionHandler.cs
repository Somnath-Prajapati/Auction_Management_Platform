using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.UpdateAuction
{
    public class UpdateAuctionHandler : IRequestHandler<UpdateAuctionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAuctionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            return true;
        }
    }
}