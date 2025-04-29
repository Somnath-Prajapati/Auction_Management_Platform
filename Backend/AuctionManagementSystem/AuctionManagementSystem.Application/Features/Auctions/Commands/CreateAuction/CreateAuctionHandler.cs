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

namespace AuctionManagementSystem.Application.Features.Auctions.Commands.CreateAuction
{
    public class CreateAuctionHandler : IRequestHandler<CreateAuctionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAuctionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
        {
            // Mapping from CreateAuctionCommand to TblAuction
            var auction = _mapper.Map<TblAuction>(request);

            // Add the auction to the database
            await _unitOfWork.AuctionRepository.AddAsync(auction);

            // Save changes
            await _unitOfWork.SaveAsync();

            // Return the AuctionId
            return auction.AuctionId;
        }
    }
}
