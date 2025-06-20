using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Dtos.Reports;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Features.Reports.Queries.GetFilteredTransactions
{
    public class GetFilteredTransactionsHandler : IRequestHandler<GetFilteredTransactionsQuery, FilteredTransactionResultDto>
    {
        private readonly IReports _reports;

        public GetFilteredTransactionsHandler(IReports reports)
        {
            _reports = reports;
        }

        public async Task<FilteredTransactionResultDto> Handle(GetFilteredTransactionsQuery request, CancellationToken cancellationToken)
        {
            return await _reports.GetFilteredSortedTransactionsAsync(
                request.Page,
                request.PageSize,
                request.StatusId,
                request.CardTypeId,
                request.TransactionTypeId,
                request.PaymentMethodId,
                request.SearchText,
                request.SortColumn,
                request.SortDirection);
        }
    }
}
