using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TTEcommerce.Application.Dtos;

namespace TTEcommerce.Blazor.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/products");
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{id}");
        }

        public async Task CreateProductAsync(ProductDto product)
        {
            await _httpClient.PostAsJsonAsync("api/products", product);
        }

        public async Task UpdateProductAsync(string id, ProductDto product)
        {
            await _httpClient.PutAsJsonAsync($"api/products/{id}", product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/products/{id}");
        }
    }
}