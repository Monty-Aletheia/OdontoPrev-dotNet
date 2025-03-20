using ClaimService.Application.Services;
using ClaimService.Application.Services.HttpClients;
using ClaimService.Application.Services.HttpClients.Interface;
using ClaimService.Application.Services.Profiles;
using ClaimService.Domain.Interfaces;
using ClaimService.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClaimService
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
			services.AddScoped<IClaimRepository, ClaimRepository>();

			// Profile
			services.AddAutoMapper(typeof(ClaimProfile));

			// Services
			services.AddScoped<ClaimAppService>();

			services.AddHttpClient<IConsultationHttpClient, ConsultationServiceHttpClient>();

			return services;
		}
	}
}