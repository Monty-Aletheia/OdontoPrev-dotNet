using AuthService.Application.Interfaces;
using AuthService.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace AuthService.Application.Services
{
	public class AuthAppService : IAuthAppService
	{
		private readonly ITokenService _tokenService;
		private readonly IDentistHttpClient _dentistHttpClient;
		private readonly ILogger<AuthAppService> _logger;

		public AuthAppService(
			ITokenService tokenService,
			IDentistHttpClient dentistHttpClient,
			ILogger<AuthAppService> logger)
		{
			_tokenService = tokenService;
			_dentistHttpClient = dentistHttpClient;
			_logger = logger;
		}

		public async Task<string> LoginAsync(Login dto)
		{
			var response = await _dentistHttpClient.PostAsync(dto, "validate");

			if (!response.IsSuccessStatusCode)
			{
				_logger.LogWarning("Failed login attempt for registration number {RegistrationNumber}.", dto.RegistrationNumber);
				throw new UnauthorizedAccessException("Invalid credentials.");
			}

			var dentist = await response.Content.ReadFromJsonAsync<DentistResponseDTO>();

			// Você pode usar dados do payload aqui se quiser incluir no token
			return _tokenService.GenerateToken(dentist);
		}

		public async Task RegisterAsync(Signup dto)
		{
			var response = await _dentistHttpClient.PostAsync(dto);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();
				_logger.LogError("Failed to register dentist with registration number {RegistrationNumber}: {Error}.", dto.RegistrationNumber, error);
				throw new Exception($"Error registering dentist: {error}");
			}
		}
	}
}