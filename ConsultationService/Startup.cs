﻿using ConsultationService.Application.Services;
using ConsultationService.Application.Services.HttpClients;
using ConsultationService.Application.Services.HttpClients.Interfaces;
using ConsultationService.Application.Services.Profiles;
using ConsultationService.Domain.Interfaces;
using ConsultationService.Infra.Data;
using ConsultationService.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService
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
			services.AddScoped<IConsultationRepository, ConsultationRepository>();

			// Profile
			services.AddAutoMapper(typeof(ConsultationProfile));

			// Services
			services.AddScoped<ConsultationAppService>();

			// HttpClients
			services.AddHttpClient<IDentistHttpClient, DentistServiceHttpClient>()
				.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
				{
					ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
				});

			services.AddHttpClient<IPatientHttpClient, PatientServiceHttpClient>()
				.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
				{
					ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
				});


			return services;
		}
	}
}