using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.AuditTrial;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AuditTrail.Queries.GetAllAuditTrails
{
    public class GetAllAuditTrailsQuery : IRequest<List<AuditTrailDto>> { }

}
