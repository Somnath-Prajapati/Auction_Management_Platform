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
    public class GetDirectSaleRevenueQueryHandler : IRequestHandler<GetDirectSaleRevenueQuery, List<DirectSaleMonthlyRevenueDto>>
    {
        private readonly IReports _reports;

        public GetDirectSaleRevenueQueryHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<List<DirectSaleMonthlyRevenueDto>> Handle(GetDirectSaleRevenueQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetDirectSaleRevenueAsync(request.ViewByMode);
        }
    }

}
