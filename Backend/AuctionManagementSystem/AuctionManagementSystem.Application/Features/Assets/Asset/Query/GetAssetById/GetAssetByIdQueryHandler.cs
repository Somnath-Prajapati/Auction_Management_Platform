using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetAssetById
{
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, GetAssetsDto>
    {
        private readonly IAssetsRepository _assetsRepository;
        private readonly IMapper _mapper;

        public GetAssetByIdQueryHandler(IMapper mapper, IAssetsRepository assetsRepository)
        {
            _mapper = mapper;
            _assetsRepository = assetsRepository;
        }

        public async Task<GetAssetsDto> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var asset = await _assetsRepository.GetByIdAsync(request.id);

            return _mapper.Map<GetAssetsDto>(asset);
        }
    }
}
