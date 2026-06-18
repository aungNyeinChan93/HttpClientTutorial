using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HttpClient.domain.Features.Dapper
{
    public class DapperService : IDapperService
    {
        private readonly IConfiguration _configuration;
        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("GameStore"));
            connection.Open();
            return connection;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        {
            using var conn = GetOpenConnection();
            return await conn.QueryAsync<T>(sql, param);
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            using var conn = GetOpenConnection();
            return await conn.ExecuteAsync(sql, param);
        }
    }
}
