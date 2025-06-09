using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Reports;
using AuctionManagementSystem.Application.Dtos.Reports;
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

        public async Task<List<DirectSaleAssetWithMediaDto>> GetDirectSaleAssetsWithMediaAsync()
        {
            var results = new List<DirectSaleAssetWithMediaDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetDirectSaleAssetsWithMedia", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(new DirectSaleAssetWithMediaDto
                            {
                                AssetId = reader.GetInt32(reader.GetOrdinal("AssetId")),
                                Title = reader["Title"] as string,
                                CategoryId = reader["CategoryId"] as int?,
                                Deposit = reader["Deposit"] as decimal?,
                                SellerId = reader["SellerId"] as int?,
                                Commission = reader["Commission"] as decimal?,
                                StartingPrice = reader["StartingPrice"] as decimal?,
                                IncrementalTime = reader["IncrementalTime"] as int?,
                                MinIncrement = reader["MinIncrement"] as decimal?,
                                MakeOffer = reader["MakeOffer"] as bool?,
                                Featured = reader["Featured"] as bool?,
                                AwardingId = reader["AwardingId"] as int?,
                                StatusId = reader["StatusId"] as int?,
                                VATId = reader["VATId"] as int?,
                                VATPercent = reader["VATPercent"] as decimal?,
                                CourtCaseNumber = reader["CourtCaseNumber"] as string,
                                RegistrationDeadline = reader["RegistrationDeadline"] as DateTime?,
                                Description = reader["Description"] as string,
                                MapLatitude = reader["MapLatitude"] as string,
                                MapLongitude = reader["MapLongitude"] as string,
                                AdminFees = reader["AdminFees"] as decimal?,
                                AuctionFees = reader["AuctionFees"] as decimal?,
                                BuyerCommission = reader["BuyerCommission"] as decimal?,
                                WinnerId = reader["WinnerId"] as int?,
                                AssetNumber = reader["AssetNumber"] as string,
                                RequestForViewing = reader["RequestForViewing"] as bool?,
                                RequestForInquiry = reader["RequestForInquiry"] as bool?,
                                GalleryFilePaths = reader["GalleryFilePaths"] as string,
                                DocumentFilePaths = reader["DocumentFilePaths"] as string
                            });
                        }
                    }
                }
            }

            return results;
        }

        public async Task<StatementAccountResultDto> GetStatementOfAccountAsync(int? userId, int? statusId)
        {
            var result = new StatementAccountResultDto
            {
                Transactions = new List<StatementTransactionDto>(),
                TotalTransactions = 0
            };

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("GetStatementOfAccount", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", (object?)userId ?? DBNull.Value);
            command.Parameters.AddWithValue("@StatusId", (object?)statusId ?? DBNull.Value);

            using var reader = await command.ExecuteReaderAsync();

            // Read transaction rows
            while (await reader.ReadAsync())
            {
                result.Transactions.Add(new StatementTransactionDto
                {
                    TransactionId = reader.GetInt32(0),
                    TransactionNumber = reader.GetString(1),
                    TransactionDateTime = reader.GetDateTime(2),
                    Amount = reader.GetDecimal(3),
                    TransactionTypeId = reader.GetInt32(4),
                    TransactionTypeName = reader.GetString(5),
                    PaymentMethodId = reader.GetInt32(6),
                    PaymentMethodName = reader.GetString(7),
                    StatusId = reader.GetInt32(8),
                    StatusName = reader.GetString(9),
                    Notes = reader.IsDBNull(10) ? null : reader.GetString(10),
                    UserId = reader.GetInt32(11),
                    UserName = reader.GetString(12)
                });
            }

            // Read total count
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.TotalTransactions = reader.GetInt32(0);
            }

            return result;
        }

        public async Task<RefundTransactionResultDto> GetAllRefundRequestsAsync()
        {
            var result = new RefundTransactionResultDto();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("GetAllRefundRequests", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await command.ExecuteReaderAsync();

            // Read transaction list
            while (await reader.ReadAsync())
            {
                var dto = new RefundTransactionDto
                {
                    TransactionId = reader.GetInt32(0),
                    TransactionNumber = reader.GetString(1),
                    Amount = reader.GetDecimal(2),
                    UserId = reader.GetInt32(3),
                    TransactionTypeId = reader.GetInt32(4),
                    PaymentMethodId = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                    CardTypeId = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                    MerchantTransactionId = reader.IsDBNull(7) ? null : reader.GetString(7),
                    TransactionDateTime = reader.GetDateTime(8),
                    StatusId = reader.GetInt32(9),
                    Notes = reader.IsDBNull(10) ? null : reader.GetString(10),
                    DocumentPath = reader.IsDBNull(11) ? null : reader.GetString(11),
                    CreatedByAdminID = reader.IsDBNull(12) ? null : reader.GetInt64(12),
                    CreatedAt = reader.GetDateTime(13),
                    UpdatedAt = reader.IsDBNull(14) ? null : reader.GetDateTime(14),
                    CreatedBy = reader.IsDBNull(15) ? null : reader.GetInt32(15),
                    CreatedDate = reader.IsDBNull(16) ? null : reader.GetDateTime(16),
                    UpdatedBy = reader.IsDBNull(17) ? null : reader.GetInt32(17),
                    UpdatedDate = reader.IsDBNull(18) ? null : reader.GetDateTime(18),
                    DeletedBy = reader.IsDBNull(19) ? null : reader.GetInt32(19),
                    DeletedDate = reader.IsDBNull(20) ? null : reader.GetDateTime(20),
                    IsDeleted = reader.GetBoolean(21)
                };


                result.Transactions.Add(dto);
            }

            // Read count
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.RefundRequestCount = reader.GetInt32(0);
            }

            return result;
        }

        public async Task<DepositTransactionResultDto> GetLatestDepositsAsync()
        {
            var result = new DepositTransactionResultDto();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("GetLatestDeposits", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await command.ExecuteReaderAsync();

            // Read transactions
            while (await reader.ReadAsync())
            {
                var dto = new DepositTransactionDto
                {
                    TransactionId = reader.GetInt32(0),
                    TransactionNumber = reader.GetString(1),
                    Amount = reader.GetDecimal(2),
                    UserId = reader.GetInt32(3),
                    TransactionTypeId = reader.GetInt32(4),
                    PaymentMethodId = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                    CardTypeId = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                    MerchantTransactionId = reader.IsDBNull(7) ? null : reader.GetString(7),
                    TransactionDateTime = reader.GetDateTime(8),
                    StatusId = reader.GetInt32(9),
                    Notes = reader.IsDBNull(10) ? null : reader.GetString(10),
                    DocumentPath = reader.IsDBNull(11) ? null : reader.GetString(11),
                    CreatedByAdminID = reader.IsDBNull(12) ? null : reader.GetInt64(12),
                    CreatedAt = reader.GetDateTime(13),
                    UpdatedAt = reader.IsDBNull(14) ? null : reader.GetDateTime(14),
                    CreatedBy = reader.IsDBNull(15) ? null : reader.GetInt32(15),
                    CreatedDate = reader.IsDBNull(16) ? null : reader.GetDateTime(16),
                    UpdatedBy = reader.IsDBNull(17) ? null : reader.GetInt32(17),
                    UpdatedDate = reader.IsDBNull(18) ? null : reader.GetDateTime(18),
                    DeletedBy = reader.IsDBNull(19) ? null : reader.GetInt32(19),
                    DeletedDate = reader.IsDBNull(20) ? null : reader.GetDateTime(20),
                    IsDeleted = reader.GetBoolean(21)
                };

                result.Transactions.Add(dto);
            }

            // Read count
            if (await reader.NextResultAsync() && await reader.ReadAsync())
            {
                result.DepositTransactionCount = reader.GetInt32(0);
            }

            return result;
        }


    }
}
