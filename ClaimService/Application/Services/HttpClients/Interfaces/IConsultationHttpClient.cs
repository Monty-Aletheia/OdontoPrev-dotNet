namespace ClaimService.Application.Services.HttpClients.Interface
{
	public interface IConsultationHttpClient
	{
		public Task<HttpResponseMessage> GetAsync(string url);
	}
}