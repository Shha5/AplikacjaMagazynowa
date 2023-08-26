using Dapper;
using DataAccessLibrary.SqlAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccessLibrary.SqlAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionString = "AppDataDbConnection")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString)))
            {
                return await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionString = "AppDataDbConnection")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString)))
            {
               return await connection.ExecuteAsync(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            }
            
        }
    }
}
