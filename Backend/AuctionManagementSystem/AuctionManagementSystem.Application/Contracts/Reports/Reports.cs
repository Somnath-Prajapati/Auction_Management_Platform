using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;

namespace AuctionManagementSystem.Application.Contracts.Reports
{
    public interface IReports
    {
        Task<List<HighBiddingCustomerDto>> GetHighBiddingLimitCustomersAsync(int? userId);
        Task<List<MonthlyRevenueDto>> GetAuctionMonthlyRevenueAsync();
        Task<List<MonthlyRevenueDto>> GetDirectSaleMonthlyRevenueAsync();
    }
}
