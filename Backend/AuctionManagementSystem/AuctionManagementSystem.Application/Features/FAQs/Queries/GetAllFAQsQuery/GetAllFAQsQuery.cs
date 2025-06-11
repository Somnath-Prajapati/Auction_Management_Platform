using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.FAQs.Queries.GetAllFAQsQuery
{
    public record GetAllFAQsQuery : IRequest<IEnumerable <FaqDto>>
    {

    }
}
