using AuthService.Application.Interfaces;
using AuthService.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Application.Services
{

	public class TokenService : ITokenService
	{
		private readonly JwtSettings _jwtSettings;

		public TokenService(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}

		public string GenerateToken(DentistResponseDTO dentist)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
		new Claim(ClaimTypes.Name, dentist.Name),
		new Claim("id", dentist.Id.ToString()),
		new Claim("registrationNumber", dentist.RegistrationNumber),
		new Claim("specialty", dentist.Specialty),
		new Claim("riskStatus", dentist.RiskStatus.ToString()),
		new Claim("claimsRate", dentist.ClaimsRate?.ToString() ?? "0")
	};

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}


	}
}