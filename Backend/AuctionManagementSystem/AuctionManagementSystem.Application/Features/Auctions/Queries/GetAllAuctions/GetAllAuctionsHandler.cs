using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Dtos.Auctions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Application.Features.Auctions.Queries.GetAllAuctions
{
    public class GetAllAuctionsHandler : IRequestHandler<GetAllAuctionsQuery, IEnumerable<AuctionDto>>
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public GetAllAuctionsHandler(IAuctionRepository auctionRepository, IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuctionDto>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
        {
            var auctions = await _auctionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuctionDto>>(auctions);
        }

        //public async Task<List<AuctionDto>> Handle(GetAllAuctionsQuery request, CancellationToken cancellationToken)
        //{
        //var auctions = await _auctionRepository.GetAllAsync();
        //    return _mapper.Map<List<AuctionDto>>(auctions);
        //}
    }
}
