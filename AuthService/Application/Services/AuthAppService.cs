using AuthService.Application.Interfaces;
using AuthService.Domain.Models;

namespace AuthService.Application.Services
{

	public class AuthAppService : IAuthAppService
	{
		private readonly ITokenService _tokenService;
		private readonly IDentistHttpClient _dentistHttpClient;

		public AuthAppService(ITokenService tokenService, IDentistHttpClient dentistHttpClient)
		{
			_tokenService = tokenService;
			_dentistHttpClient = dentistHttpClient;
		}

		public async Task<string> LoginAsync(Login dto)
		{
			var response = await _dentistHttpClient.PostAsync(dto, "validate");

			if (!response.IsSuccessStatusCode)
			{
				throw new UnauthorizedAccessException("Invalid credentials.");
			}

			// Se quiser recuperar algum dado do dentista, pode deserializar:
			// var dentist = await response.Content.ReadFromJsonAsync<DentistDTO>();

			return _tokenService.GenerateToken(dto.RegistrationNumber);
		}

		public async Task RegisterAsync(Signup dto)
		{
			var response = await _dentistHttpClient.PostAsync(dto);

			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadAsStringAsync();
				throw new Exception($"Error registering dentist: {error}");
			}
		}
	}
}