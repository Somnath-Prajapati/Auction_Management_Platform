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
    public class GetLatestDepositQueryHandler : IRequestHandler<GetLatestDepositQuery, DepositTransactionResultDto>
    {
        private readonly IReports _reports;

        public GetLatestDepositQueryHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<DepositTransactionResultDto> Handle(GetLatestDepositQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetLatestDepositsAsync();
        }
    }

}
