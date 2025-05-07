using Shared.Common.Dtos;

namespace MlNetWorker.Services.Interfaces
{
	public interface IPredictionResultSenderService
	{
		Task SendResultAsync(PredictionResultDTO dto);
	}
}
