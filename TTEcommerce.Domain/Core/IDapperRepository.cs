using System.Collections.Generic;
using System.Threading.Tasks;

namespace TTEcommerce.Domain.Core
{
    public interface IDapperRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string tableName, string id);
        Task<int> CreateAsync(string tableName, T entity);
        Task<bool> UpdateAsync(string tableName, T entity);
        Task<bool> DeleteAsync(string tableName, string id);
        Task<IEnumerable<T>> QueryAsync(string sql, object parameters = null);
        Task<T> QueryFirstOrDefaultAsync(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
    }
}