using ConsultationService.Application.Dtos;
using ConsultationService.Application.Services.HttpClients.Interfaces;
using System;
using System.Net.Http;

namespace ConsultationService.Application.Services.HttpClients
{
	public class DentistServiceHttpClient : IDentistHttpClient
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;
		private readonly string _baseUrl;

		public DentistServiceHttpClient(HttpClient client, IConfiguration configuration)
		{
			_client = client;
			_configuration = configuration;
			_baseUrl = _configuration["DentistService"];
		}

		public async Task<HttpResponseMessage> ValidateDentistAsync(string url)
		{
			var fullUrl = $"{_baseUrl}/{url}";
			return await _client.GetAsync(fullUrl);
		}

		public async Task<IEnumerable<DentistResponseDTO>> GetDentistsByIdsAsync(IEnumerable<Guid> ids)
		{
			var fullUrl = $"{_baseUrl}/by-ids";

			var body = new
			{
				dentistIds = ids
			};

			var content = JsonContent.Create(body);

			var response = await _client.PostAsync(fullUrl, content);

			response.EnsureSuccessStatusCode();

			var dentists = await response.Content.ReadFromJsonAsync<IEnumerable<DentistResponseDTO>>();

			return dentists ?? new List<DentistResponseDTO>();
		}



	}
}