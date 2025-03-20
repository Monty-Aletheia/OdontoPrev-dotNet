using ConsultationService.Application.Services.HttpClients.Interfaces;

namespace ConsultationService.Application.Services.HttpClients
{
	public class DentistServiceHttpClient : IDentistHttpClient
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;

		public DentistServiceHttpClient(HttpClient client, IConfiguration configuration)
		{
			_configuration = configuration;

			// Ignora a validação do certificado SSL
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			_client = new HttpClient(handler)
			{
				BaseAddress = new Uri(_configuration["DentistService"])
			};
		}

		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			return await _client.GetAsync(_configuration["DentistService"]);
		}


	}
}