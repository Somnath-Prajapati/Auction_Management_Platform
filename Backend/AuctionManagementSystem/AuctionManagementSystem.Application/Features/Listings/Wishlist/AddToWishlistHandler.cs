using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Features.Listings.Cart;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Listings.Wishlist
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
            return await _wishlistRepository.AddAsync(request.Dto);
        }
    }
}
