using Aletheia.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aletheia.Presentation.Config
{
	public static class ServiceConfigs
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<ClaimService>();
			services.AddScoped<ConsultationService>();
			services.AddScoped<DentistService>();
			services.AddScoped<PatientService>();

			return services;
		}
	}
}