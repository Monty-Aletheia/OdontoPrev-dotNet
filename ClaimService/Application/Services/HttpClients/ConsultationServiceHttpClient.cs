using ClaimService.Application.Services.HttpClients.Interfaces;

namespace ClaimService.Application.Services.HttpClients
{
	public class ConsultationServiceHttpClient : IConsultationHttpClient
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;

		public ConsultationServiceHttpClient(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
		}

		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			var fullUrl = $"{_configuration["ConsultationService"]}/{url}";
			return await _client.GetAsync(fullUrl);
		}


	}
}