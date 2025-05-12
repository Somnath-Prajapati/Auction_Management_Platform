using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Listings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Listings.Cart
{
    public class AddToCartCommand : IRequest<bool>
    {
        public AddToCartDto Dto { get; set; }
    }
}
