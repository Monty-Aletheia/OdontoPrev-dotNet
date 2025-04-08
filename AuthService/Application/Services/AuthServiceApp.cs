namespace AuthService.Application.Services
{
	using global::AuthService.Application.Services.HttpClients.Interfaces;
	using global::AuthService.Domain.Models;

	namespace AuthService.Application.Services
	{
		public class AuthService 
		{
			private readonly TokenService _tokenService;
			private readonly IDentistHttpClient _dentistHttpClient;

			public AuthService(TokenService tokenService, IDentistHttpClient dentistHttpClient)
			{
				_tokenService = tokenService;
				_dentistHttpClient = dentistHttpClient;
			}

			public async Task<string> LoginAsync(Login dto)
			{
				var response = await _dentistHttpClient.PostAsync("auth/validate", dto);

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
				var response = await _dentistHttpClient.PostAsync("dentists", dto);

				if (!response.IsSuccessStatusCode)
				{
					var error = await response.Content.ReadAsStringAsync();
					throw new Exception($"Error registering dentist: {error}");
				}
			}
		}
	}

}
