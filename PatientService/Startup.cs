using Microsoft.EntityFrameworkCore;
using PatientService.Application.Services;
using PatientService.Application.Services.Profiles;
using PatientService.Domain.Interfaces;
using PatientService.Infra.Data;
using PatientService.Infra.Repositories;

namespace PatientService
{
	public static class Startup
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{

			// Database Configuration
			var connectionString = configuration.GetConnectionString("OracleFIAPDbContext");

			services.AddDbContext<FIAPDbContext>(options =>
				options.UseOracle(connectionString));

			// Repositories
			services.AddScoped<IPatientRepository, PatientRepository>();

			// Profile
			services.AddAutoMapper(typeof(PatientProfile));

			// Services
			services.AddScoped<PatientAppService>();

			services.AddHealthChecks()
			  .AddDbContextCheck<FIAPDbContext>("Database");

			return services;
		}
	}
}