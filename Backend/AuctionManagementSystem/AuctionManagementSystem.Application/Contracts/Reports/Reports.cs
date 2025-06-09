using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Reports;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;

namespace AuctionManagementSystem.Application.Contracts.Reports
{
    public interface IReports
    {
        Task<List<HighBiddingCustomerDto>> GetHighBiddingLimitCustomersAsync(int? userId);
        Task<List<MonthlyRevenueDto>> GetAuctionMonthlyRevenueAsync();
        Task<List<MonthlyRevenueDto>> GetDirectSaleMonthlyRevenueAsync();

        Task<List<DirectSaleAssetWithMediaDto>> GetDirectSaleAssetsWithMediaAsync();

        Task<StatementAccountResultDto> GetStatementOfAccountAsync(int? userId, int? statusId);

        Task<RefundTransactionResultDto> GetAllRefundRequestsAsync();

        Task<DepositTransactionResultDto> GetLatestDepositsAsync();



    }
}
