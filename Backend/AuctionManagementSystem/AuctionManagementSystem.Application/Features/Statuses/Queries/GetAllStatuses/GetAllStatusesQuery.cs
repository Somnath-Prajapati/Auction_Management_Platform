using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Statuses;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Statuses.Queries.GetAllStatuses
{
    public record GetAllStatusesQuery : IRequest<List<StatusDto>>;

    
    
}
