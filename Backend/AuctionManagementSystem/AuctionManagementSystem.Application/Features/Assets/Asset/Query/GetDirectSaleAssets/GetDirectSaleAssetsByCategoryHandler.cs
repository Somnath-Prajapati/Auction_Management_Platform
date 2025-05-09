using AutoMapper;
using MediatR;

using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetDirectSaleAssets;

namespace Application.Features.DirectSale.Handlers
{
    public class GetDirectSaleAssetsByCategoryHandler : IRequestHandler<GetDirectSaleAssetsByCategoryQuery, List<DirectSaleAssetDto>>
    {
        private readonly IAssetsRepository _assetRepository;
        private readonly IMapper _mapper;

        public GetDirectSaleAssetsByCategoryHandler(IAssetsRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        public async Task<List<DirectSaleAssetDto>> Handle(GetDirectSaleAssetsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var assets = await _assetRepository.GetAllAsync(a =>
                a.CategoryId == request.CategoryId && a.IsAvailableForDirectSale);

            return _mapper.Map<List<DirectSaleAssetDto>>(assets);
        }
    }
}
