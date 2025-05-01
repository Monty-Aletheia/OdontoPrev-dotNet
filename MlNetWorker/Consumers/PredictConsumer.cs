using MassTransit;
using MlNetWorker.Models;
using MlNetWorker.Services;

namespace MlNetWorker.Consumers
{
	public class PredictConsumer : IConsumer<InputData>
	{
		private readonly PredictionService _service;

		public PredictConsumer(PredictionService service)
		{
			_service = service;
		}

		public async Task Consume(ConsumeContext<InputData> context)
		{
			var result = _service.Predict(context.Message);
			Console.WriteLine($"[Predição] Score: {result.Score}");

			await context.RespondAsync(result);
		}
	}

}
