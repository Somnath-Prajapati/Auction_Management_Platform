using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Persistence.Repositories.Listings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Listings.Cart
{
    public class AddToCartHandler : IRequestHandler<AddToCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public AddToCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            return await _cartRepository.AddAsync(request.Dto);
        }
    }
}
