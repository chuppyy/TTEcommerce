using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using TTEcommerce.Application.Dtos;

namespace TTEcommerce.Blazor.Services
{
    public class CategoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaginatedResult<CategoryDto>> GetPaginatedCategoriesAsync(int pageNumber, int pageSize)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            var response = await httpClient.GetStringAsync($"api/category?pageNumber={pageNumber}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<PaginatedResult<CategoryDto>>(response);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            var response = await httpClient.GetStringAsync($"api/category/{id}");
            return JsonConvert.DeserializeObject<CategoryDto>(response);
        }

        public async Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string searchTerm)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            var response = await httpClient.GetStringAsync($"api/category/search?searchTerm={searchTerm}");
            return JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(response);
        }

        public async Task CreateCategoryAsync(CategoryDto category)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            await httpClient.PostAsync("api/category", content);
        }

        public async Task UpdateCategoryAsync(string id, CategoryDto category)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            await httpClient.PutAsync($"api/category/{id}", content);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("TTEcommerceClient");
            await httpClient.DeleteAsync($"api/category/{id}");
        }
    }
}