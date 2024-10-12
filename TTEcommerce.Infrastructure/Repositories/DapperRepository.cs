using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TTEcommerce.Domain.Core
{
    public class DapperRepository<T> : IDapperRepository<T> where T : class
    {
        private readonly string _connectionString;

        public DapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<T> GetByIdAsync(string tableName, string id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {tableName} WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> CreateAsync(string tableName, T entity)
        {
            var insertQuery = GenerateInsertQuery(tableName);

            using var connection = CreateConnection();
            return await connection.ExecuteAsync(insertQuery, entity);
        }

        public async Task<bool> UpdateAsync(string tableName, T entity)
        {
            var updateQuery = GenerateUpdateQuery(tableName);

            using var connection = CreateConnection();
            var affected = await connection.ExecuteAsync(updateQuery, entity);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(string tableName, string id)
        {
            using var connection = CreateConnection();
            var affected = await connection.ExecuteAsync($"DELETE FROM {tableName} WHERE Id = @Id", new { Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<T> QueryFirstOrDefaultAsync(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }
        
        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object parameters = null)
        {
            var connection = CreateConnection();
            return await connection.QueryAsync<TResult>(sql, parameters);
        }

        public async Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object parameters = null)
        {
            var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<TResult>(sql, parameters);
        }


        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, parameters);
        }

        private string GenerateInsertQuery(string tableName)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.Name != "Id" && !p.PropertyType.IsClass)
                .Select(p => p.Name);

            var columns = string.Join(", ", properties);
            var values = string.Join(", ", properties.Select(p => $"@{p}"));

            return $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        }

        private string GenerateUpdateQuery(string tableName)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.Name != "Id" && !p.PropertyType.IsClass)
                .Select(p => $"{p.Name} = @{p.Name}");

            var updateSet = string.Join(", ", properties);

            return $"UPDATE {tableName} SET {updateSet} WHERE Id = @Id";
        }
    }
}