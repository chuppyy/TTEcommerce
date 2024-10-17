using TTEcommerce.Domain.ProductAggregate;
using TTEcommerce.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using TTEcommerce.Domain.Core;

namespace TTEcommerce.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepository<T>(this IServiceCollection services) where T : Entity
        {
            services.AddScoped<IRepository<T>, Repository<T>>();
        }

        public static void AddDapperRepository<T>(this IServiceCollection services) where T : class
        {
            services.AddScoped<IDapperRepository<T>, DapperRepository<T>>();
        }
    }
}