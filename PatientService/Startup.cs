using MassTransit;
using Microsoft.EntityFrameworkCore;
using PatientService.Application.Services;
using PatientService.Application.Services.Interfaces;
using PatientService.Application.Services.Profiles;
using PatientService.Domain.Interfaces;
using PatientService.Infra.Data;
using PatientService.Infra.Messaging;
using PatientService.Infra.Repositories;
using Serilog;

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

			// RabbitMQ

			services.AddMassTransit(x =>
			{
				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host("rabbitmq://localhost", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});
				});
			});

			services.AddScoped<IMessagePublisher, RabbitMqPublisher>();


			// Services
			services.AddScoped<IPatientAppService, PatientAppService>();
			services.AddScoped<IPredictionMessageService, PredictionMessageService>();



			services.AddHealthChecks()
			  .AddDbContextCheck<FIAPDbContext>("Database");

			return services;
		}

	}

}