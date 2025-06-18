using System.Collections.Generic;
using AuctionManagementSystem.Application.Dtos.Requests;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestStatuses
{
    public class GetAllRequestStatusesQuery : IRequest<List<RequestStatusDto>>
    {
    }
} 