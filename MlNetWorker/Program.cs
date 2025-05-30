using MassTransit;
using MlNetWorker.Consumers;
using MlNetWorker.Services;
using MlNetWorker.Services.Interfaces;
using MlNetWorker.AI; 

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IPredictionResultSenderService, PredictionResultSenderService>();

builder.Services.AddSingleton<PredictionService>();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<PredictConsumer>();

	x.UsingRabbitMq((ctx, cfg) =>
	{
		cfg.Host("rabbitmq", "/", h =>
		{
			h.Username("admin");
			h.Password("admin");
		});

		cfg.ReceiveEndpoint("predict-queue", e =>
		{
			e.ConfigureConsumer<PredictConsumer>(ctx);
		});
	});
});

var host = builder.Build();

if (args.Contains("generate-ai"))
{
	var modelBuilder = new ModelBuilder();
	modelBuilder.generateAI();
}
else
{
	host.Run();
}

host.Run();
