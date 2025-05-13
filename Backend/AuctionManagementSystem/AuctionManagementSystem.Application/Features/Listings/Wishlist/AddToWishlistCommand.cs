using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Listings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Listings.Cart
{
    public class AddToWishlistCommand : IRequest<bool>
    {
        public AddToWishlistDto Dto { get; set; }
    }
}
