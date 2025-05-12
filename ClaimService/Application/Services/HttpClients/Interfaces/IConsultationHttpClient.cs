namespace ClaimService.Application.Services.HttpClients.Interfaces
{
	public interface IConsultationHttpClient
	{
		public Task<HttpResponseMessage> GetAsync(string url);
	}
}