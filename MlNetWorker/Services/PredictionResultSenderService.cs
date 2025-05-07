using MassTransit;
using MlNetWorker.Services.Interfaces;
using Shared.Common.Dtos;

namespace MlNetWorker.Services
{
	public class PredictionResultSenderService : IPredictionResultSenderService
	{
		private readonly ISendEndpointProvider _sendEndpointProvider;

		public PredictionResultSenderService(ISendEndpointProvider sendEndpointProvider)
		{
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task SendResultAsync(PredictionResultDTO dto)
		{
			var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:prediction-result-queue"));
			await endpoint.Send(dto);
		}
	}
}
