using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using TTEcommerce.Application.Dtos;

namespace TTEcommerce.Blazor.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>("api/categories");
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryDto>($"api/categories/{id}");
        }

        public async Task CreateCategoryAsync(CategoryDto category)
        {
            await _httpClient.PostAsJsonAsync("api/categories", category);
        }

        public async Task UpdateCategoryAsync(string id, CategoryDto category)
        {
            await _httpClient.PutAsJsonAsync($"api/categories/{id}", category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/categories/{id}");
        }
    }
}