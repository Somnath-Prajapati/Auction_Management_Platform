using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Auctions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Queries.GetAuctionsByIds
{
    public record GetAuctionsByIdsQuery(List<int> AuctionIds) : IRequest<List<AuctionDto>>;
}
