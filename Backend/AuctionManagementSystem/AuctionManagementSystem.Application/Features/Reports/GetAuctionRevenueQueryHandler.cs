using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Reports
{
    public class GetAuctionRevenueQueryHandler : IRequestHandler<GetAuctionRevenueQuery, List<MonthlyRevenueDto>>
    {
        private readonly IReports _reports;

        public GetAuctionRevenueQueryHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<List<MonthlyRevenueDto>> Handle(GetAuctionRevenueQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetAuctionMonthlyRevenueAsync();
        }
    }

}
