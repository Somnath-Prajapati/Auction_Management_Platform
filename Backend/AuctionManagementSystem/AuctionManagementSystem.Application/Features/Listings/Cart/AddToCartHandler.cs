using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Listings.Cart;
using AuctionManagementSystem.Domain.Entities;
using AuctionManagementSystem.Domain.Interfaces;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Cart
{
    //public class AddToCartHandler : IRequestHandler<AddToCartCommand, bool>
    //{
    //    private readonly ICartRepository _cartRepository;

    //    public AddToCartHandler(ICartRepository cartRepository)
    //    {
    //        _cartRepository = cartRepository;
    //    }

    //    public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    //    {
    //        return await _cartRepository.AddOrUpdateCartItemAsync(request.UserId, request.AssetId, request.Quantity);
    //    }
    //}

    //public class AddToCartHandler : IRequestHandler<AddToCartCommand, bool>
    //{
    //    private readonly ICartRepository _cartRepository;

    //    public AddToCartHandler(ICartRepository cartRepository)
    //    {
    //        _cartRepository = cartRepository;
    //    }

    //    public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    //    {
    //        var alreadyExists = await _cartRepository.ExistsAsync(request.UserId, request.AssetId);
    //        if (alreadyExists)
    //            return false;

    //        return await _cartRepository.AddToCartAsync(request.UserId, request.AssetId);
    //    }
    //}

    //public class GetCartByUserIdHandler : IRequestHandler<GetCartByUserIdQuery, List<DirectSaleAssetDto>>
    //{
    //    private readonly ICartRepository _cartRepository;

    //    public GetCartByUserIdHandler(ICartRepository cartRepository)
    //    {
    //        _cartRepository = cartRepository;
    //    }

    //    public async Task<List<DirectSaleAssetDto>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    //    {
    //        return await _cartRepository.GetCartByUserIdAsync(request.UserId);
    //    }
    //}

    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public RemoveFromCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            return await _cartRepository.RemoveFromCartAsync(request.UserId, request.AssetId);
        }
    }

    public class DecreaseCartItemQuantityHandler : IRequestHandler<DecreaseCartItemQuantityCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public DecreaseCartItemQuantityHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(DecreaseCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            return await _cartRepository.DecreaseCartItemQuantityAsync(request.UserId, request.AssetId);
        }
    }

    public class AddToCartHandler : IRequestHandler<AddToCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IDirectSaleSettingsRepository _settingsRepository;

        public AddToCartHandler(ICartRepository cartRepository, IDirectSaleSettingsRepository settingsRepository)
        {
            _cartRepository = cartRepository;
            _settingsRepository = settingsRepository;
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var settings = await _settingsRepository.GetByIdAsync(1);
            var timeLimit = settings.CartTimerInMinutes ?? 0;

            bool alreadyInCart = await _cartRepository.ExistsAsync(request.UserId, request.AssetId, timeLimit);
            if (alreadyInCart)
                return false;
                //throw new Exception("Asset is already in the cart.");

            bool heldByAnother = await _cartRepository.IsAssetHeldByAnotherUserAsync(request.UserId, request.AssetId, timeLimit);
            if (heldByAnother)
                throw new Exception("Asset is currently held by another user.");

            return await _cartRepository.AddToCartAsync(request.UserId, request.AssetId, timeLimit);
        }
    }


    public class GetCartByUserIdHandler : IRequestHandler<GetCartByUserIdQuery, List<DirectSaleAssetDto>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IAssetsRepository _assetRepository;
        private readonly IDirectSaleSettingsRepository _settingsRepository;
        private readonly IMapper _mapper;

        public GetCartByUserIdHandler(ICartRepository cartRepository, IDirectSaleSettingsRepository settingsRepository, IMapper mapper, IAssetsRepository assetRepository)
        {
            _cartRepository = cartRepository;
            _assetRepository = assetRepository;
            _settingsRepository = settingsRepository;
            _mapper = mapper;
        }

        public async Task<List<DirectSaleAssetDto>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            var settings = await _settingsRepository.GetByIdAsync(1);
            var timeLimit = settings.CartTimerInMinutes ?? 10; // default to 10 minutes
            var now = DateTime.UtcNow;

            var expiredItems = await _cartRepository.GetExpiredCartItemsAsync(request.UserId, timeLimit);
            if (expiredItems.Any())
            {
                foreach (var item in expiredItems)
                {
                    item.IsActive = false;
                    item.DeletedDate = now;
                }
                await _cartRepository.UpdateRangeAsync(expiredItems);
            }

            var validCartItems = await _cartRepository.GetValidCartItemsAsync(request.UserId, timeLimit);

            // Map from the underlying asset, not the cart item
            var assetList = validCartItems
                .Where(ci => ci.Asset != null)
                .Select(ci => ci.Asset)
                .ToList();

            return _mapper.Map<List<DirectSaleAssetDto>>(assetList);

        }

    }

}
