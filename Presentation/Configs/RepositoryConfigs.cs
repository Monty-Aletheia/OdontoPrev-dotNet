using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Repositories;

namespace Aletheia.Presentation.Config
{
    public static class RepositoryConfigs
    {
        public static IServiceCollection AddInfraRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IConsultationRepository, ConsultationRepository>();
            services.AddScoped<IDentistRepository, DentistRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}
