using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.Reports;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Reports
{
    public class GetAuctionReportHandler : IRequestHandler<GetAuctionReportQuery, AuctionReportResultDto>
    {
        private readonly IReports _reports;

        public GetAuctionReportHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<AuctionReportResultDto> Handle(GetAuctionReportQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetAuctionReportAsync(request.ReportType);
        }
    }

}
