namespace AuthService.Application.Services.HttpClients.Interfaces
{
	public interface IDentistHttpClient
	{
		public Task<HttpResponseMessage> GetAsync(string url);
	}
}