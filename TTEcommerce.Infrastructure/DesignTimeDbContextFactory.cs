using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TTEcommerce.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Server=localhost;Database=TTEcommerce;User=devops;Password=Abcd1234;";
            builder.UseMySql(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
