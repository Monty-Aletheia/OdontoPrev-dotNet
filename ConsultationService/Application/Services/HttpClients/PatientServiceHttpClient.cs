using ConsultationService.Application.Services.HttpClients.Interfaces;
using System.Text;
using System.Text.Json;

namespace ConsultationService.Application.Services.HttpClients
{
	public class PatientServiceHttpClient : IPatientHttpClient
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;

		public PatientServiceHttpClient(HttpClient client, IConfiguration configuration)
		{
			_configuration = configuration;

			// Ignora a validação do certificado SSL
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			_client = new HttpClient(handler)
			{
				BaseAddress = new Uri(_configuration["PatientService"])
			};
		}

		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			return await _client.GetAsync(_configuration["PatientService"]);
		}


	}
}