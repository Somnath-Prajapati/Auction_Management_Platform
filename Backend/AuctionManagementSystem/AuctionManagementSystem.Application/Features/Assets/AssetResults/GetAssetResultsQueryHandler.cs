using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetResults
{
    public class GetAssetResultsQueryHandler : IRequestHandler<GetAssetResultsQuery, AssetResultsDto>
    {
        private readonly IAssetsRepository _repository;

        public GetAssetResultsQueryHandler(IAssetsRepository repository)
        {
            _repository = repository;
        }

        public async Task<AssetResultsDto> Handle(GetAssetResultsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAssetResultsAsync(request.AssetId);
        }
    }

}
