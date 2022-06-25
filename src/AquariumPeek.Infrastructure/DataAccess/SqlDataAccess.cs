using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AquariumPeek.Infrastructure.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _configuration;

        public SqlDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string storedProcedure,
            U paramters,
            string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId)))
                return await connection.QueryAsync<T>(storedProcedure, paramters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(
            string storedProcedure,
            T paramters,
            string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId)))
                await connection.ExecuteAsync(storedProcedure, paramters, commandType: CommandType.StoredProcedure);
        }
    }
}
