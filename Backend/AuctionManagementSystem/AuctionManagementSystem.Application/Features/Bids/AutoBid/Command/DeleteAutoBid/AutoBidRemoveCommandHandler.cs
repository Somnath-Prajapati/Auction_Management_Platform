using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Command.DeleteAutoBid
{
    public class AutoBidRemoveCommandHandler : IRequestHandler<AutoBidRemoveCommand>
    {
        private readonly IAutoBidRepository _autoBidRepository;
        private readonly IMapper _mapper;
        public AutoBidRemoveCommandHandler(IAutoBidRepository autoBidRepository,IMapper mapper)
        {
            _autoBidRepository = autoBidRepository;
            _mapper = mapper;
        }

        public async Task Handle(AutoBidRemoveCommand request, CancellationToken cancellationToken)
        {
            var autoBid = await _autoBidRepository.GetByUserAuctionAssetAsync(request.userId,request.auctionId, request.assetId);

            if (autoBid == null)
            {
                throw new NotFoundException("Auto-bid not found for the specified user, auction, and asset.");
            }

            autoBid.IsActive = false;
            autoBid.UpdatedDate = DateTime.UtcNow;
            await _autoBidRepository.UpdateAutoBidAsync(autoBid);
        }

       
    }
}
