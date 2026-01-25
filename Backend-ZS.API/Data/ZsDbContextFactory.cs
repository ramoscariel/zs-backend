using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Backend_ZS.API.Data
{
    public class ZsDbContextFactory : IDesignTimeDbContextFactory<ZsDbContext>
    {
        public ZsDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var conn =
                configuration.GetConnectionString("ZSConnectionString")
                ?? configuration.GetConnectionString("ZsConnection")
                ?? configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException("Falta ConnectionString: 'ZSConnectionString' (o fallback).");

            var options = new DbContextOptionsBuilder<ZsDbContext>()
                .UseSqlServer(conn)
                .Options;

            return new ZsDbContext(options);
        }
    }
}
