using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Reports.Queries.GetFilteredTransactions
{
    public class GetFilteredTransactionsQuery : IRequest<FilteredTransactionResultDto>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? StatusId { get; set; } = null;
        public int? CardTypeId { get; set; } = null;
        public int? TransactionTypeId { get; set; } = null;
        public int? PaymentMethodId { get; set; } = null;
        public string SearchText { get; set; } = string.Empty;
        public string SortColumn { get; set; } = "TransactionDateTime";
        public string SortDirection { get; set; } = "asc";
    }
}
