namespace AuthService.Application.Interfaces
{
	public interface IDentistHttpClient
	{
		public Task<HttpResponseMessage> GetAsync(string url);
		Task<HttpResponseMessage> PostAsync<T>(T data, string? url = null);
	}
}