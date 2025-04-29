using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.RequestsDtos;
using AuctionManagementSystem.Domain.Entities.Request;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequests
{
    public class GetAllRequestQuery : IRequest<IEnumerable<RequestDto>>
    {
    }
}