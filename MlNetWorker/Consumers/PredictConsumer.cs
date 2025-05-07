using MassTransit;
using MlNetWorker.Services;
using MlNetWorker.Services.Interfaces;
using Shared.Common.Dtos;

namespace MlNetWorker.Consumers
{
	public class PredictConsumer : IConsumer<PatientRiskAssessmentDTO>
	{
		private readonly PredictionService _service;
		private readonly IPredictionResultSenderService _resultSender;

		public PredictConsumer(PredictionService service, IPredictionResultSenderService resultSender)
		{
			_service = service;
			_resultSender = resultSender;
		}

		public async Task Consume(ConsumeContext<PatientRiskAssessmentDTO> context)
		{
			Console.WriteLine($"[Predição] Recebendo mensagem: {context.Message}");
			var result = _service.Predict(context.Message);
			Console.WriteLine($"[Predição] Score: {result.Score}");

			var response = new PredictionResultDTO
			{
				PatientID = context.Message.PatientID,
				RiskScore = result.Score,
			};

			await _resultSender.SendResultAsync(response); 
		}
	}

}