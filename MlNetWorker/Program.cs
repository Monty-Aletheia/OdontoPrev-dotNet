using MassTransit;
using MlNetWorker;
using MlNetWorker.Consumers;
using MlNetWorker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<PredictionService>();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<PredictConsumer>();

	x.UsingRabbitMq((ctx, cfg) =>
	{
		cfg.Host("rabbitmq://localhost");

		cfg.ReceiveEndpoint("predict-queue", e =>
		{
			e.ConfigureConsumer<PredictConsumer>(ctx);
		});
	});
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();


host.Run();
