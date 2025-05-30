using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Dtos.Auctions;
using AuctionManagementSystem.Application.Exceptions;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Queries.GetAuctionById
{
    public class GetAuctionByIdHandler : IRequestHandler<GetAuctionByIdQuery, AuctionDto>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public GetAuctionByIdHandler(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<AuctionDto> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
        {
            var auction = await _auctionRepository.GetByIdAsync(request.AuctionId);

            if (auction == null)
                throw new NotFoundException($"Auction with ID {request.AuctionId} not found."); // or custom NotFoundException

            return _mapper.Map<AuctionDto>(auction);
        }
    }
}
