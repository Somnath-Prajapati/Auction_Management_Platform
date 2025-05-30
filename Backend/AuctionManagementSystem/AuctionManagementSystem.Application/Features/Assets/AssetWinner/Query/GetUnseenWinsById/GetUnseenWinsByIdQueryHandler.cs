using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetWinner.Query.GetUnseenWinsById
{
    public class GetUnseenWinsByIdQueryHandler : IRequestHandler<GetUnseenWinsByIdQuery, List<AssetWinnerDto>>
    {
        private readonly IAssetWinnerRepository _assetWinnerRepository;
        private readonly IMapper _mapper;
        public GetUnseenWinsByIdQueryHandler(IAssetWinnerRepository assetWinnerRepository, IMapper mapper)
        {
            _assetWinnerRepository = assetWinnerRepository;
            _mapper = mapper;
        }
        public async Task<List<AssetWinnerDto>> Handle(GetUnseenWinsByIdQuery request, CancellationToken cancellationToken)
        {
            var unseenWins = await _assetWinnerRepository.GetUnseenWinsByUserIdAsync(request.UserId);
            var result = _mapper.Map<List<AssetWinnerDto>>(unseenWins);
            return result;
          
        }
    }
}
