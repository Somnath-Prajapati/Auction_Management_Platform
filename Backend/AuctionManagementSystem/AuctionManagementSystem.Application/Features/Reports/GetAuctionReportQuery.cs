using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Reports;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Reports
{
    public record GetAuctionReportQuery(string ReportType) : IRequest<AuctionReportResultDto>;

    public class AuctionReportResultDto
    {
        public int TotalCount { get; set; }
        public List<AuctionReportDto> Auctions { get; set; }
    }

}
