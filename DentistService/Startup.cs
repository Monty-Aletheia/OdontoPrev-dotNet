using DentistService.Application.Services;
using DentistService.Application.Services.Interfaces;
using DentistService.Application.Services.Profiles;
using DentistService.Domain.Interfaces;
using DentistService.Infra.Data;
using DentistService.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentistService
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
			services.AddScoped<IDentistRepository, DentistRepository>();

			// Profile
			services.AddAutoMapper(typeof(DentistProfile));

			// Services
			services.AddScoped<IDentistAppService,DentistAppService>();

			services.AddHealthChecks()
			  .AddDbContextCheck<FIAPDbContext>("Database");


			return services;
		}
	}
}