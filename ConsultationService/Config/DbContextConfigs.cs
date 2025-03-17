using ConsultationService.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService.Config
{
    public static class DbContextConfigs
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OracleFIAPDbContext");

            IServiceCollection serviceCollection = services.AddDbContext<FIAPDbContext>(options =>
                options.UseOracle(connectionString));

            return services;
        }
    }
}
