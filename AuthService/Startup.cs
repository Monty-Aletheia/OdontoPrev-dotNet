using AuthService.Application.Interfaces;
using AuthService.Application.Services;
using AuthService.Application.Services.HttpClients;
using AuthService.Domain.Models;

namespace AuthService
{
	public static class Startup
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			// HttpClient
			services.AddHttpClient<IDentistHttpClient, DentistServiceHttpClient>();

			// JWT Settings
			services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

			// Services
			services.AddScoped<IAuthAppService, AuthAppService>();
			services.AddScoped<ITokenService, TokenService>();



			return services;
		}
	}
}