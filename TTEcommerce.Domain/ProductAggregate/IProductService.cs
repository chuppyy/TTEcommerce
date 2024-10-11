using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTEcommerce.Domain.ProductAggregate
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(string id, ProductDto productDto);
        Task DeleteProductAsync(string id);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId);
    }
}
