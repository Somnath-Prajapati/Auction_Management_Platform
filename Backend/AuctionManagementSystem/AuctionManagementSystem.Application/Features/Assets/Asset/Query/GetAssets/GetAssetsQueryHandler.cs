using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Query.GetAssets
{
    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, IEnumerable<GetAssetsDto>>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        public GetAssetsQueryHandler(IAssetsRepository assetsRepository, IMapper mapper)
        {
            _assetsRepository = assetsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAssetsDto>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            var asset = await _assetsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAssetsDto>>(asset);
        }   



        //public async Task<IEnumerable<GetAssetsDto>> IRequestHandler<GetAssetsQuery, IEnumerable<GetAssetsDto>>.Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        //{
        
        //}
    }
}
