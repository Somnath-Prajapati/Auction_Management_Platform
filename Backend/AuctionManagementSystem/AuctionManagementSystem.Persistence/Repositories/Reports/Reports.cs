using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Dtos.Listings;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AuctionManagementSystem.Persistence.Repositories.Reports
{
    public class Reports : IReports
    {
        private readonly string _connectionString;

        public Reports(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<HighBiddingCustomerDto>> GetHighBiddingLimitCustomersAsync(int? userId)
        {
            var result = new List<HighBiddingCustomerDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetHighBiddingLimitCustomers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var userIdParam = new SqlParameter("@UserId", SqlDbType.Int)
                    {
                        Value = userId.HasValue ? (object)userId.Value : DBNull.Value
                    };
                    command.Parameters.Add(userIdParam);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var dto = new HighBiddingCustomerDto
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                                TotalManualBids = reader.GetInt32(reader.GetOrdinal("TotalManualBids")),
                                TotalManualBidAmount = reader.GetDecimal(reader.GetOrdinal("TotalManualBidAmount")),
                                TotalAutoBids = reader.GetInt32(reader.GetOrdinal("TotalAutoBids")),
                                TotalAutoBidAmount = reader.GetDecimal(reader.GetOrdinal("TotalAutoBidAmount")),
                                TotalBidsCount = reader.GetInt32(reader.GetOrdinal("TotalBidsCount")),
                                TotalBidAmount = reader.GetDecimal(reader.GetOrdinal("TotalBidAmount")),
                            };
                            result.Add(dto);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<MonthlyRevenueDto>> GetAuctionMonthlyRevenueAsync()
        {
            var revenueList = new List<MonthlyRevenueDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetAuctionAssetsMonthlyRevenue", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var dto = new MonthlyRevenueDto
                            {
                                Year = reader.GetInt32(0),
                                Month = reader.GetInt32(1),
                                TotalAmount = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2)
                            };

                            revenueList.Add(dto);
                        }
                    }
                }
            }

            return revenueList;
        }

        public async Task<List<MonthlyRevenueDto>> GetDirectSaleMonthlyRevenueAsync()
        {
            var revenueList = new List<MonthlyRevenueDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetDirectSaleAssetsMonthlyRevenue", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var dto = new MonthlyRevenueDto
                            {
                                Year = reader.GetInt32(0),
                                Month = reader.GetInt32(1),
                                TotalAmount = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2)
                            };

                            revenueList.Add(dto);
                        }
                    }
                }
            }

            return revenueList;
        }
    }
}
