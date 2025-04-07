using AuthService.Application.Services.HttpClients.Interfaces;

namespace AuthService.Application.Services.HttpClients
{
	public class DentistServiceHttpClient : IDentistHttpClient
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;

		public DentistServiceHttpClient(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
		}

		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			var fullUrl = $"{_configuration["DentistService"]}/{url}";
			return await _client.GetAsync(fullUrl);
		}

	}
}