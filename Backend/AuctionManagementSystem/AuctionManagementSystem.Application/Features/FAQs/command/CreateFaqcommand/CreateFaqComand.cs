using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.FAQs.command.CreateFaqCommand
{
    public record CreateFaqComand(FaqDto Dto) : IRequest<FaqDto>
    {
    }
}
