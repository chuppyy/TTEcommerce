using System.Collections.Generic;
using System.Threading.Tasks;
using TTEcommerce.Application.Dtos;

namespace TTEcommerce.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryByIdAsync(string id);
        Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string searchTerm);
        Task<IEnumerable<CategoryDto>> GetPaginatedCategoriesAsync(int pageNumber, int pageSize);
        Task CreateCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(string id, CategoryDto categoryDto);
        Task DeleteCategoryAsync(string id);
    }
}