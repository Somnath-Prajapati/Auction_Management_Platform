using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos;
using AuctionManagementSystem.Application.Features.FAQs.Queries.GetFAQsQuery;

namespace AuctionManagementSystem.Application.Contracts.FAQ
{
    public interface IGetAllFAQ
    {
        Task<List<GetFaqDto>> GeAllCategory();
        Task<IEnumerable<FaqDto>> GetAllFAQ();
    }
}
