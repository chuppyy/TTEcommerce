using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Domain.ProductAggregate
{
    public class ProductService : IProductService
    {
        private readonly IDapperRepository<Product> _dapperRepository;
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductService(
            IDapperRepository<Product> dapperRepository,
            IRepository<Product> repository,
            IRepository<Category> categoryRepository)
        {
            _dapperRepository = dapperRepository;
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        // Read operations using Dapper
        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _dapperRepository.QuerySingleOrDefaultAsync(
                @"SELECT p.*, c.Name AS CategoryName 
                FROM Products p 
                JOIN Categories c ON p.CategoryId = c.Id 
                WHERE p.Id = @Id AND p.IsDeleted = 0",
                new { Id = id });
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _dapperRepository.QueryAsync(
                @"SELECT p.*, c.Name AS CategoryName 
                FROM Products p
                JOIN Categories c ON p.CategoryId = c.Id
                WHERE p.IsDeleted = 0 AND (
                    p.Name LIKE @SearchTerm OR
                    p.Description LIKE @SearchTerm OR
                    c.Name LIKE @SearchTerm
                )",
                new { SearchTerm = $"%{searchTerm}%" });
        }

        public async Task<IEnumerable<Product>> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            return await _dapperRepository.QueryAsync(
                @"SELECT p.*, c.Name AS CategoryName 
                FROM Products p
                JOIN Categories c ON p.CategoryId = c.Id
                WHERE p.IsDeleted = 0
                ORDER BY p.CreatedAt DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY",
                new { Offset = (pageNumber - 1) * pageSize, PageSize = pageSize });
        }

        // Write operations using IRepository<Product>
        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var category = await _categoryRepository.GetByIdAsync(productDto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Invalid category ID");
            }

            var product = new Product(
                productDto.Name,
                productDto.Description,
                productDto.ImageUrl,
                productDto.NetWeight,
                productDto.CategoryId
            );

            _repository.Add(product);
            await Task.CompletedTask; // Simulating async operation
            return product;
        }

        public async Task UpdateProductAsync(string id, ProductDto productDto)
        {
            var product = await Task.FromResult(_repository.GetById(id));
            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException("Product not found");
            }

            var category = await Task.FromResult(_categoryRepository.GetById(productDto.CategoryId));
            if (category == null)
            {
                throw new ArgumentException("Invalid category ID");
            }

            product.UpdateDetails(
                productDto.Name,
                productDto.Description,
                productDto.ImageUrl,
                productDto.NetWeight,
                productDto.CategoryId
            );

            await Task.CompletedTask; // Simulating async operation
        }

        public async Task DeleteProductAsync(string id)
        {
            var product = await Task.FromResult(_repository.GetById(id));
            if (product == null || product.IsDeleted)
            {
                throw new ArgumentException("Product not found");
            }

            product.Delete();
            await Task.CompletedTask; // Simulating async operation
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId)
        {
            return await _dapperRepository.QueryAsync(
                @"SELECT p.*, c.Name AS CategoryName 
                      FROM Products p
                      JOIN Categories c ON p.CategoryId = c.Id
                      WHERE p.CategoryId = @CategoryId AND p.IsDeleted = 0",
                new { CategoryId = categoryId });
        }
    }
}
