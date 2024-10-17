using TTEcommerce.Application.Interfaces;
using TTEcommerce.Application.Services;
using TTEcommerce.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TTEcommerce.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}