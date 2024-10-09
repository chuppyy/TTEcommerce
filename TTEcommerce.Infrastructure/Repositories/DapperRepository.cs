using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TTEcommerce.Domain.Core
{
    public class DapperRepository<T> : IDapperRepository<T> where T : class
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public DapperRepository(IConfiguration configuration, string tableName)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _tableName = tableName;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
        }

        public async Task<T> GetByIdAsync(string id)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> CreateAsync(T entity)
        {
            var insertQuery = GenerateInsertQuery();

            using var connection = CreateConnection();
            return await connection.ExecuteAsync(insertQuery, entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var updateQuery = GenerateUpdateQuery();

            using var connection = CreateConnection();
            var affected = await connection.ExecuteAsync(updateQuery, entity);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using var connection = CreateConnection();
            var affected = await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<T> QuerySingleOrDefaultAsync(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, parameters);
        }

        private string GenerateInsertQuery()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.Name != "Id" && !p.PropertyType.IsClass)
                .Select(p => p.Name);

            var columns = string.Join(", ", properties);
            var values = string.Join(", ", properties.Select(p => $"@{p}"));

            return $"INSERT INTO {_tableName} ({columns}) VALUES ({values})";
        }

        private string GenerateUpdateQuery()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.Name != "Id" && !p.PropertyType.IsClass)
                .Select(p => $"{p.Name} = @{p.Name}");

            var updateSet = string.Join(", ", properties);

            return $"UPDATE {_tableName} SET {updateSet} WHERE Id = @Id";
        }
    }
}
