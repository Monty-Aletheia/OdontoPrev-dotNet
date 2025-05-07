using MassTransit;
using PatientService.Application.Services.Interfaces;
using Shared.Common.Dtos;

namespace PatientService.Infra.Messaging.Consumers
{
	public class PredictionResultConsumer : IConsumer<PredictionResultDTO>
	{
		private readonly IPatientAppService _patientService;

		public PredictionResultConsumer(IPatientAppService patientService)
		{
			_patientService = patientService;
		}

		public async Task Consume(ConsumeContext<PredictionResultDTO> context)
		{
			var result = context.Message;

			Console.WriteLine($"[PredictionResultConsumer] Received result: PatientID={result.PatientID}, Score={result.RiskScore}");

			await _patientService.SavePredictionResultAsync(result);
		}
	}
}
