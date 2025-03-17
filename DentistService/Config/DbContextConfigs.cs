using DentistService.Data;
using Microsoft.EntityFrameworkCore;

namespace DentistService.Config
{
    public static class DbContextConfigs
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OracleFIAPDbContext");

            services.AddDbContext<FIAPDbContext>(options =>
                options.UseOracle(connectionString));

            return services;
        }
    }
}
