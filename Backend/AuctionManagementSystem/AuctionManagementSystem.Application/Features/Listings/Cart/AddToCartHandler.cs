using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Listings.Cart;
using AuctionManagementSystem.Persistence.Repositories.Listings;
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

    public class AddToCartHandler : IRequestHandler<AddToCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public AddToCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _cartRepository.ExistsAsync(request.UserId, request.AssetId);
            if (alreadyExists)
                return false;

            return await _cartRepository.AddToCartAsync(request.UserId, request.AssetId);
        }
    }

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

    public class GetCartByUserIdHandler : IRequestHandler<GetCartByUserIdQuery, List<DirectSaleAssetDto>>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartByUserIdHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<DirectSaleAssetDto>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _cartRepository.GetCartByUserIdAsync(request.UserId);
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

}
