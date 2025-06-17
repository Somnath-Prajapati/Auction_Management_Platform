using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetAssetWithTranslationById
{

    public class GetAssetWithTranslationByIdQueryHandler : IRequestHandler<GetAssetWithTranslationByIdQuery, GetAssetsFormTranslatedDto>
    {
        private readonly IAssetsRepository _assetRepository;

        public GetAssetWithTranslationByIdQueryHandler(IAssetsRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<GetAssetsFormTranslatedDto> Handle(GetAssetWithTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _assetRepository.GetByIdAllDetailsAsync(request.AssetId);
        }
    }

}
