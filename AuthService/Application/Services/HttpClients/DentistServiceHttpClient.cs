using AuthService.Application.Interfaces;
using AuthService.Domain.Models;
using System.Net.Http.Json;

namespace AuthService.Application.Services.HttpClients
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
			_baseUrl = configuration["DentistService"];
		}

		public async Task<HttpResponseMessage> GetAsync(string url)
		{
			var fullUrl = $"{_baseUrl}/{url}";
			return await _client.GetAsync(fullUrl);
		}

		public async Task<HttpResponseMessage> PostAsync<T>(T data, string? url = null)
		{
			var fullUrl = string.IsNullOrEmpty(url)
				? $"{_baseUrl}"
				: $"{_baseUrl}/{url}";

			return await _client.PostAsJsonAsync(fullUrl, data);
		}

	}
}