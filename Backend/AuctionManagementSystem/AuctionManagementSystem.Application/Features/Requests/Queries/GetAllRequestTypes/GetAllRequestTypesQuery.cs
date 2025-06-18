using System.Collections.Generic;
using AuctionManagementSystem.Application.Dtos.Requests;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Queries.GetAllRequestTypes
{
    public class GetAllRequestTypesQuery : IRequest<List<RequestTypeDto>>
    {
    }
} 