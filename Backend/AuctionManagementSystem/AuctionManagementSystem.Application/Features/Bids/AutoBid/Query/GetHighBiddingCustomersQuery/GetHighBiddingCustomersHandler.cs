using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Bids;
using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Dtos.Reports;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Bids.AutoBid.Query.GetHighBiddingCustomersQuery
{
    public class GetHighBiddingCustomersHandler : IRequestHandler<GetHighBiddingCustomersQuery, List<HighBiddingCustomerDto>>
    {
        private readonly IReports _reports;

        public GetHighBiddingCustomersHandler(IReports reports)
        {
            _reports = reports ?? throw new ArgumentNullException(nameof(reports));
        }

        public async Task<List<HighBiddingCustomerDto>> Handle(GetHighBiddingCustomersQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            // Pass cancellationToken if repository supports it in future
            var result = await _reports.GetHighBiddingLimitCustomersAsync(request.UserId);

            // Optionally: Return an empty list if null (to avoid null reference in consumers)
            return result ?? new List<HighBiddingCustomerDto>();
        }
    }

}
