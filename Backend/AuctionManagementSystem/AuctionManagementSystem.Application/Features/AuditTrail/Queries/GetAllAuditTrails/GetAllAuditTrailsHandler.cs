using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Dtos.AuditTrial;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.AuditTrail.Queries.GetAllAuditTrails
{
    public class GetAllAuditTrailsHandler : IRequestHandler<GetAllAuditTrailsQuery, List<AuditTrailDto>>
    {
        private readonly IAuditTrailRepository _repository;
        private readonly IMapper _mapper;

        public GetAllAuditTrailsHandler(IAuditTrailRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AuditTrailDto>> Handle(GetAllAuditTrailsQuery request, CancellationToken cancellationToken)
        {
            var auditTrails = await _repository.GetAllAsync();
            return _mapper.Map<List<AuditTrailDto>>(auditTrails);
        }
    }

}
