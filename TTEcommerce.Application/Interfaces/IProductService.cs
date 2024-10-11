using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTEcommerce.Application.Dtos;
using TTEcommerce.Domain.ProductAggregate;

namespace TTEcommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<IEnumerable<ProductDto>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string categoryId);
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(string id, ProductDto productDto);
        Task DeleteProductAsync(string id);
    }
}
