using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Dtos.Reports;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Reports
{
    public class GetRefundRequestQueryHandler : IRequestHandler<GetRefundRequestQuery, RefundTransactionResultDto>
    {
        private readonly IReports _reports;

        public GetRefundRequestQueryHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<RefundTransactionResultDto> Handle(GetRefundRequestQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetAllRefundRequestsAsync();
        }
    }

}
