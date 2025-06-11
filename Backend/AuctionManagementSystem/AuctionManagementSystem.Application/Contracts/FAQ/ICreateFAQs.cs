using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Application.Contracts.FAQ
{
    public interface ICreateFAQs
    {
        Task<ActionResult<FaqDto>> CreateFaq(FaqDto faqDto);
        Task<ActionResult<FaqDto>> UpdateFaq(int id);
        Task<ActionResult<FaqDto>> DeleteFaq(int id);
    }
}
