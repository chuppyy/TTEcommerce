using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTEcommerce.Domain.Core
{
    public interface IDapperRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<int> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<T>> QueryAsync(string sql, object parameters = null);
        Task<T> QuerySingleOrDefaultAsync(string sql, object parameters = null);
        Task<int> ExecuteAsync(string sql, object parameters = null);
    }
}
