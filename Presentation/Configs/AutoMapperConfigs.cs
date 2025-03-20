using Aletheia.Application.Services.Profiles;

namespace Aletheia.Presentation.Config
{
	public static class AutoMapperConfigs
	{
		public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ClaimProfile), typeof(ConsultationProfile), typeof(DentistProfile), typeof(PatientProfile));
			return services;
		}
	}
}