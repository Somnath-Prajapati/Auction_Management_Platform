using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;


namespace AuctionManagementSystem.Persistence.Repositories.Reports
{
    public class DapperHelper
    {
        private readonly string _connectionString;

        public DapperHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string spName, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string spName, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<(T1, List<T2>)> QueryMultipleAsync<T1, T2>(string spName, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var multi = await connection.QueryMultipleAsync(spName, parameters, commandType: CommandType.StoredProcedure);

            var second = (await multi.ReadAsync<T2>()).ToList();     // Read transactions first
            var first = await multi.ReadFirstAsync<T1>();            // Then read total count


            return (first, second);
        }

        //public async Task<List<T>> QueryListAsync<T>(string spName, object parameters = null)
        //{
        //    var result = await QueryAsync<T>(spName, parameters);
        //    return result.ToList();
        //}

        public async Task<List<T>> QueryListAsync<T>(string spName, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryAsync<T>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure);

            return result.AsList(); // avoids LINQ .ToList() call
        }


        public async Task<int> ExecuteAsync(string spName, object parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }

}
