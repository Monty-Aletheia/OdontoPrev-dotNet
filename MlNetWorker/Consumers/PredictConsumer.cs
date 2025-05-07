using MassTransit;
using MlNetWorker.Services;
using Shared.Common.Dtos;

namespace MlNetWorker.Consumers
{
	public class PredictConsumer : IConsumer<PatientRiskAssessmentDTO>
	{
		private readonly PredictionService _service;

		public PredictConsumer(PredictionService service)
		{
			Console.WriteLine("✅ PredictConsumer inicializado.");
			_service = service;
		}

		public async Task Consume(ConsumeContext<PatientRiskAssessmentDTO> context)
		{
			Console.WriteLine($"[Predição] Recebendo mensagem: {context.Message}");
			var result = _service.Predict(context.Message);
			Console.WriteLine($"[Predição] Score: {result.Score}");

			await context.RespondAsync(result);
		}
	}

}