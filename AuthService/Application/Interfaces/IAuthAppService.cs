using AuthService.Domain.Models;

namespace AuthService.Application.Interfaces
{
	public interface IAuthAppService
	{
		Task<string> LoginAsync(Login dto);
		Task RegisterAsync(Signup dto);
	}
}