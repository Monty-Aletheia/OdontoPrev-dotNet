namespace ConsultationService.Application.Services.HttpClients.Interfaces
{
	public interface IPatientHttpClient
	{
		public Task<HttpResponseMessage> GetAsync(string url);
	}
}