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

    public class GetDirectSaleAssetsQueryHandler : IRequestHandler<GetDirectSaleAssetsQuery, List<DirectSaleAssetWithMediaDto>>
    {
        private readonly IReports _reports;

        public GetDirectSaleAssetsQueryHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<List<DirectSaleAssetWithMediaDto>> Handle(GetDirectSaleAssetsQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetDirectSaleAssetsWithMediaAsync();
        }
    }
}
