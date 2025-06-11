using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.FAQ;
using AuctionManagementSystem.Application.Dtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.FAQs.Queries.GetAllFAQsQuery
{
    public class GetAllFAQsQueryHandler : IRequestHandler<GetAllFAQsQuery, IEnumerable<FaqDto>>
    {
        readonly IGetAllFAQ _getAllCategory;
        public GetAllFAQsQueryHandler(IGetAllFAQ getAllCategory) { 
            _getAllCategory = getAllCategory;

        }
        public async Task<IEnumerable<FaqDto>> Handle(GetAllFAQsQuery request, CancellationToken cancellationToken)
        {
            return await _getAllCategory.GetAllFAQ();
            
        }
    }
}
