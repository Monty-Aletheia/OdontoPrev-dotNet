namespace AuthService.Application.Controllers
{
	using AuthService.Application.Interfaces;
	using AuthService.Domain.Models;
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthAppService _authAppService;

		public AuthController(IAuthAppService authAppService)
		{
			_authAppService = authAppService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] Login dto)
		{
			try
			{
				var token = await _authAppService.LoginAsync(dto);
				return Ok(new { token });
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized(new { message = "Invalid credentials." });
			}
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] Signup dto)
		{
			try
			{
				await _authAppService.RegisterAsync(dto);
				return Ok(new { message = "Dentist successfully registered!" });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = $"Registration failed: {ex.Message}" });
			}
		}
	}

}