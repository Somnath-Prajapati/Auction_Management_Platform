using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Dtos.Reports;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Reports
{
    public class GetStatementAccountQueryHandler : IRequestHandler<GetStatementAccountQuery, StatementAccountResultDto>
    {
        private readonly IReports _reports;

        public GetStatementAccountQueryHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<StatementAccountResultDto> Handle(GetStatementAccountQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetStatementOfAccountAsync(request.UserId, request.StatusId);
        }
    }

}
