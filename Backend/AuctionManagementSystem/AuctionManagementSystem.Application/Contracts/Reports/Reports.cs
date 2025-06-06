using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Listings;

namespace AuctionManagementSystem.Application.Contracts.Reports
{
    public interface IReports
    {
        Task<List<HighBiddingCustomerDto>> GetHighBiddingLimitCustomersAsync(int? userId);
    }
}
