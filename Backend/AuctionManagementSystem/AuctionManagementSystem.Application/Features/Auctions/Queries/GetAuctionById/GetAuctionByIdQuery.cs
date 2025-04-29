using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Auctions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Auctions.Queries.GetAuctionById
{
    public class GetAuctionByIdQuery : IRequest<AuctionDto>
    {
        public int AuctionId { get; set; }
    }
}
