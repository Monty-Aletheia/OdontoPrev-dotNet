using Microsoft.EntityFrameworkCore;
using PatientService.Data;

namespace PatientService.Configs
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
