using Microsoft.Extensions.DependencyInjection;
using Aletheia.Application.Services;

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
