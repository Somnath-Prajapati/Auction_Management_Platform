using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Dtos.Bids;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Query.GetById
{
    public class GetAutoBidByIdQueryHandler : IRequestHandler<GetAutoBidByIdQuery, AddAutoBidDto>
    {
        private readonly IAutoBidRepository _autoBidRepo;
        private readonly IMapper _mapper;

        public GetAutoBidByIdQueryHandler(IMapper mapper, IAutoBidRepository autoBidRepo)
        {
            _mapper = mapper;
            _autoBidRepo = autoBidRepo;
        }

        public async Task<AddAutoBidDto> Handle(GetAutoBidByIdQuery request, CancellationToken cancellationToken)
        {
            var dto = await _autoBidRepo.GetUserAutoBidAsync(request.userId, request.auctionId, request.assetId);


            var result = _mapper.Map<AddAutoBidDto>(dto);
            //var result = new AddAutoBidDto
            //{
            //    UserId = dto.UserId,
            //    AuctionId = dto.AuctionId,
            //    AssetId = dto.AssetId,
            //    MaxBidAmount = dto.MaxBidAmount,
            //    IsActive = dto.IsActive  
            //};

            return result;

        }
    }
}
