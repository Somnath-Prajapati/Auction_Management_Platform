using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuctionManagementSystem.Application.Features.FAQs.Queries.GetFAQsQuery
{
    public record GetFAQsQuery:IRequest<List<GetFaqDto>>
    {
    }
}
