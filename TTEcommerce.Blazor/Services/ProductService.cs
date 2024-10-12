using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TTEcommerce.Application.Dtos;

namespace TTEcommerce.Blazor.Services
{
    public class ProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/product");
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            return await httpClient.GetFromJsonAsync<ProductDto>($"api/product/{id}");
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/product/search?searchTerm={searchTerm}");
        }

        public async Task<PaginatedResult<ProductDto>> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            return await httpClient.GetFromJsonAsync<PaginatedResult<ProductDto>>($"api/product?pageNumber={pageNumber}&pageSize={pageSize}");
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(string categoryId)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/product/category/{categoryId}");
        }

        public async Task CreateProductAsync(ProductDto product)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            await httpClient.PostAsJsonAsync("api/product", product);
        }

        public async Task UpdateProductAsync(string id, ProductDto product)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            await httpClient.PutAsJsonAsync($"api/product/{id}", product);
        }

        public async Task DeleteProductAsync(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            await httpClient.DeleteAsync($"api/product/{id}");
        }
    }
}