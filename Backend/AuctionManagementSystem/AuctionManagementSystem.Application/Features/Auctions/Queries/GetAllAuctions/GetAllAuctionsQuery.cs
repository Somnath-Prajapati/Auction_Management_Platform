using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Auctions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Queries.GetAllAuctions
{
    public class GetAllAuctionsQuery : IRequest<IEnumerable<AuctionDto>>
    {
    }
}
