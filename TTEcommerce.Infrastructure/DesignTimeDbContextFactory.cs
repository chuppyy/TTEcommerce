using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TTEcommerce.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Server=your_server;Database=your_database;User=your_user;Password=your_password;";
            builder.UseMySql(connectionString);

            return new AppDbContext(builder.Options);
        }
    }
}
