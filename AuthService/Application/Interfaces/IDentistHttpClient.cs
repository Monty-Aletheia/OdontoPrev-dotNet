using AuthService.Domain.Models;

namespace AuthService.Application.Interfaces
{
	public interface IDentistHttpClient
	{
		Task<HttpResponseMessage> GetAsync(string url);
		Task<HttpResponseMessage> PostAsync<T>(T data, string? url = null);
	}
}