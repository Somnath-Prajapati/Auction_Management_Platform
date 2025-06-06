using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Listings;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Query.GetHighBiddingCustomersQuery
{
    public record GetHighBiddingCustomersQuery(int? UserId = null) : IRequest<List<HighBiddingCustomerDto>>;
}
