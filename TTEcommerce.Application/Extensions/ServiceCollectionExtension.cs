using TTEcommerce.Domain.ProductAggregate;
using TTEcommerce.Infrastructure.Repositories;
using TTEcommerce.Application.Interfaces;
using TTEcommerce.Application.Services;
using TTEcommerce.Application.Dtos;
using TTEcommerce.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IDapperRepository<ProductDto>, DapperRepository<ProductDto>>();
            services.AddScoped<IDapperRepository<CategoryDto>, DapperRepository<CategoryDto>>();
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}