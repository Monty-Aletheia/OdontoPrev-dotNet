using MassTransit;
using Microsoft.EntityFrameworkCore;
using PatientService.Application.Dtos;
using PatientService.Application.Services;
using PatientService.Application.Services.Interfaces;
using PatientService.Application.Services.Profiles;
using PatientService.Domain.Interfaces;
using PatientService.Infra.Data;
using PatientService.Infra.Messaging.Consumers;
using PatientService.Infra.Messaging.Producers;
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

			// RabbitMQ

			services.AddMassTransit(x =>
			{
				x.AddConsumer<PredictionResultConsumer>();

				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host("rabbitmq", "/", h =>
					{
						h.Username("admin");
						h.Password("admin");
					});

					cfg.ReceiveEndpoint("prediction-result-queue", e =>
					{
						e.ConfigureConsumer<PredictionResultConsumer>(context);
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