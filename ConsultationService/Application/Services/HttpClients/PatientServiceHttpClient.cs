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
			_client = client;
			_configuration = configuration;
		}

		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			var fullUrl = $"{_configuration["PatientService"]}/{url}";
			return await _client.GetAsync(fullUrl);
		}
	}
}