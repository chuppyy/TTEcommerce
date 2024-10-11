using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TTEcommerce.Domain.Core;
using TTEcommerce.Domain.ProductAggregate;
using TTEcommerce.Infrastructure;
using TTEcommerce.Infrastructure.Repositories;
using TTEcommerce.Application.Interfaces;
using TTEcommerce.Application.Services;
using TTEcommerce.Application.Dtos;

namespace TTEcommerce.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IDapperRepository<ProductDto>, DapperRepository<ProductDto>>();
            services.AddScoped<IDapperRepository<CategoryDto>, DapperRepository<CategoryDto>>();
            services.AddDbContext<DbContext, AppDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
