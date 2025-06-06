using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Contracts.Transactions;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Transactions
{
    class GetDirectSaleRevenueQueryHandler: IRequestHandler<GetDirectSaleRevenueQuery, List<MonthlyRevenueDto>>
    {
        private readonly IReports _reports;

    public GetDirectSaleRevenueQueryHandler(IReports reports)
    {
            _reports = reports;
    }

    public async Task<List<MonthlyRevenueDto>> Handle(GetDirectSaleRevenueQuery request, CancellationToken cancellationToken)
    {
        return await _reports.GetDirectSaleMonthlyRevenueAsync();
    }
}
}
