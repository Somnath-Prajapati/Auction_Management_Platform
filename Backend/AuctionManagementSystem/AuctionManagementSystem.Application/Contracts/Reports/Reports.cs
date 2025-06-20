using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Reports;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using AuctionManagementSystem.Application.Features.Reports;

namespace AuctionManagementSystem.Application.Contracts.Reports
{
    public interface IReports
    {
        Task<List<HighBiddingCustomerDto>> GetHighBiddingLimitCustomersAsync(int? userId);
        Task<List<AuctionMonthlyRevenueDto>> GetAuctionRevenueAsync(string viewByMode);

        Task<List<DirectSaleMonthlyRevenueDto>> GetDirectSaleRevenueAsync(string viewByMode);


        Task<List<DirectSaleAssetWithMediaDto>> GetDirectSaleAssetsWithMediaAsync();

        Task<StatementAccountResultDto> GetStatementOfAccountAsync(int? userId, int? statusId);

        Task<RefundTransactionResultDto> GetAllRefundRequestsAsync();

        Task<DepositTransactionResultDto> GetLatestDepositsAsync();

        Task<AuctionReportResultDto> GetAuctionReportAsync(string reportType);

        Task<FilteredTransactionResultDto> GetFilteredSortedTransactionsAsync(
            int page,
            int pageSize,
            int? statusId,
            int? cardTypeId,
            int? transactionTypeId,
            int? paymentMethodId,
            string searchText,
            string sortColumn,
            string sortDirection
        );

    }
}
