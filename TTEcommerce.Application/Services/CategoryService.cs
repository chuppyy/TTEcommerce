using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TTEcommerce.Domain.Core;
using TTEcommerce.Domain.ProductAggregate;
using TTEcommerce.Application.Interfaces;
using TTEcommerce.Application.Dtos;
using Microsoft.Extensions.Logging;
using TTEcommerce.Application.Exceptions;

namespace TTEcommerce.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDapperRepository<CategoryDto> _dapperRepository;
        private readonly IRepository<Category> _repository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IDapperRepository<CategoryDto> dapperRepository,
            IRepository<Category> repository,
            ILogger<CategoryService> logger)
        {
            _dapperRepository = dapperRepository;
            _repository = repository;
            _logger = logger;
        }

        // Read operations using Dapper
        public async Task<CategoryDto> GetCategoryByIdAsync(string id)
        {
            return await _dapperRepository.QueryFirstOrDefaultAsync(
                @"SELECT * 
                FROM Categories 
                WHERE Id = @Id AND IsDeleted = 0",
                new { Id = id });
        }

        public async Task<IEnumerable<CategoryDto>> SearchCategoriesAsync(string searchTerm)
        {
            return await _dapperRepository.QueryAsync(
                @"SELECT * 
                FROM Categories 
                WHERE IsDeleted = 0 AND (
                    Name LIKE CONCAT('%', @SearchTerm, '%') OR
                    Description LIKE CONCAT('%', @SearchTerm, '%')
                )",
                new { SearchTerm = searchTerm });
        }

        public async Task<PaginatedResult<CategoryDto>> GetPaginatedCategoriesAsync(int pageNumber, int pageSize)
        {
            var categories = await _dapperRepository.QueryAsync(
                @"SELECT * 
                FROM Categories 
                WHERE IsDeleted = 0
                ORDER BY CreatedAt DESC
                LIMIT @PageSize OFFSET @Offset",
                new { Offset = (pageNumber - 1) * pageSize, PageSize = pageSize });

            var totalRecords = await _dapperRepository.QueryFirstOrDefaultAsync<int>(
                @"SELECT COUNT(*) 
                FROM Categories 
                WHERE IsDeleted = 0");

            return new PaginatedResult<CategoryDto>(categories, totalRecords, pageSize);
        }

        // Write operations using IRepository<Category>
        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            try
            {
                var category = new Category(
                    categoryDto.Name,
                    categoryDto.Description
                );

                _repository.Add(category);
                await Task.CompletedTask; // Simulating async operation
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a category");
                throw new ApplicationException("An error occurred while creating the category. Please try again later.", ex);
            }
        }

        public async Task UpdateCategoryAsync(string id, CategoryDto categoryDto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null || category.IsDeleted)
            {
                throw new NotFoundException($"Category with ID {id} not found");
            }

            category.UpdateDetails(
                categoryDto.Name,
                categoryDto.Description
            );

            await _repository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null || category.IsDeleted)
            {
                throw new NotFoundException($"Category with ID {id} not found");
            }

            category.Delete();
            await _repository.UpdateAsync(category);
        }
    }
}