using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Application.Features.Listings.Cart;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using AuctionManagementSystem.Application.Dtos.Assets;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Features.Wishlist
{
    public class AddToWishlistHandler : IRequestHandler<AddToWishlistCommand, bool>
    {
        private readonly IWishlistRepository _wishlistRepository;

        public AddToWishlistHandler(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<bool> Handle(AddToWishlistCommand request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _wishlistRepository.ExistsAsync(request.UserId, request.AssetId);
            if (alreadyExists)
                return false;

            return await _wishlistRepository.AddToWishlistAsync(request.UserId, request.AssetId);
        }
    }

    public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlistCommand, bool>
    {
        private readonly IWishlistRepository _wishlistRepository;

        public RemoveFromWishlistHandler(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<bool> Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken)
        {
            return await _wishlistRepository.RemoveFromWishlistAsync(request.UserId, request.AssetId);
        }
    }

    public class GetWishlistByUserIdHandler : IRequestHandler<GetWishlistByUserIdQuery, List<DirectSaleAssetDto>>
    {
        private readonly IWishlistRepository _wishlistRepository;

        public GetWishlistByUserIdHandler(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<List<DirectSaleAssetDto>> Handle(GetWishlistByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _wishlistRepository.GetWishlistAssetsByUserIdAsync(request.UserId);
        }
    }
}
