using AutoMapper;
using MediatR;

using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetDirectSaleAssets;
using AuctionManagementSystem.Domain.Interfaces;
using AuctionManagementSystem.Persistence.Repositories.Listings;

namespace Application.Features.DirectSale.Handlers
{
    public class GetDirectSaleAssetsByCategoryHandler : IRequestHandler<GetDirectSaleAssetsByCategoryQuery, List<DirectSaleAssetDto>>
    {
        private readonly IAssetsRepository _assetRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IDirectSaleSettingsRepository _settingsRepository;
        private readonly IMapper _mapper;

        public GetDirectSaleAssetsByCategoryHandler(
            IAssetsRepository assetRepository,
            ICartRepository cartRepository,
            IDirectSaleSettingsRepository settingsRepository,
            IMapper mapper)
        {
            _assetRepository = assetRepository;
            _cartRepository = cartRepository;
            _settingsRepository = settingsRepository;
            _mapper = mapper;
        }

        public async Task<List<DirectSaleAssetDto>> Handle(GetDirectSaleAssetsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var settings = await _settingsRepository.GetByIdAsync(1);
            int cartTimerInMinutes = settings?.CartTimerInMinutes ?? 0;

            var now = DateTime.UtcNow;

            //var heldAssetIds = (await _cartRepository.GetAllValidCartItemsAsync(cartTimerInMinutes))
            //    .Select(c => c.AssetId)
            //    .ToHashSet();

            //Console.WriteLine(heldAssetIds);

            // Get assets that match, then filter held ones in-memory
            var allAssets = await _assetRepository.GetAllAsync(a =>
                a.CategoryId == request.CategoryId &&
                a.IsAvailableForDirectSale &&
                !a.IsDeleted);

            //var filteredAssets = allAssets
            //    .Where(a => !heldAssetIds.Contains(a.AssetId))
            //    .ToList();

            return _mapper.Map<List<DirectSaleAssetDto>>(allAssets);
        }
    }

    public class GetAuctionAssetsByCategoryHandler : IRequestHandler<GetAuctionAssetsByCategoryQuery, List<DirectSaleAssetDto>>
    {
        private readonly IAssetsRepository _assetRepository;
        private readonly IMapper _mapper;

        public GetAuctionAssetsByCategoryHandler(IAssetsRepository assetRepository, IMapper mapper)
        {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        public async Task<List<DirectSaleAssetDto>> Handle(GetAuctionAssetsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var assets = await _assetRepository.GetAllAsync(a =>
                a.CategoryId == request.CategoryId && !a.IsAvailableForDirectSale);

            return _mapper.Map<List<DirectSaleAssetDto>>(assets);
        }
    }

}
