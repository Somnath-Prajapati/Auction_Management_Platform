using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AuctionManagementSystem.Persistence.Repositories;
using AuctionManagementSystem.Application.Contracts.FAQ;

namespace AuctionManagementSystem.Application.Features.FAQs.Queries.GetFAQsQuery
{
    public class GetFAQsQueryHandler:IRequestHandler<GetFAQsQuery, List<GetFaqDto>>
    {
        readonly IGetAllFAQ _iGetallCategory;
        public GetFAQsQueryHandler(IGetAllFAQ iGetallCategory)
        {
            _iGetallCategory = iGetallCategory;
        }

        public async Task<List<GetFaqDto>> Handle(GetFAQsQuery request, CancellationToken cancellationToken)
        {
            return await _iGetallCategory.GeAllCategory();
        }
    }
}
