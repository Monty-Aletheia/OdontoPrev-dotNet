using ConsultationService.Application.Dtos;
using ConsultationService.Application.Services.HttpClients.Interfaces;
using System.Text;
using System.Text.Json;

namespace ConsultationService.Application.Services.HttpClients
{
	public class PatientServiceHttpClient : IPatientHttpClient
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;
		private readonly string _baseUrl;

		public PatientServiceHttpClient(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
			_baseUrl = _configuration["PatientService"];
		}

		public async Task<PatientResponseDTO> GetPatientByIdAsync(Guid id)
		{
			var fullUrl = $"{_baseUrl}/patients/{id}";
			var response = await _client.GetAsync(fullUrl);

			if (!response.IsSuccessStatusCode)
			{
				throw new KeyNotFoundException($"Patient with id {id} not found.");
			}

			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<PatientResponseDTO>(content);
		}

		public async Task<HttpResponseMessage> ValidatePatientAsync(string id)
		{
			var fullUrl = $"{_baseUrl}/{id}";
			return await _client.GetAsync(fullUrl);
		}
	}
}