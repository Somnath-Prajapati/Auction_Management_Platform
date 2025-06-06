using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Dtos.Listings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AuctionManagementSystem.Persistence.Repositories.Reports
{
    class Reports:IReports
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

                    var userIdParam = new SqlParameter("@UserId", SqlDbType.Int);
                    userIdParam.Value = userId.HasValue ? (object)userId.Value : DBNull.Value;
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

    }
}
