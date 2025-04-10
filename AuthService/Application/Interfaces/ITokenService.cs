namespace AuthService.Application.Interfaces
{
	public interface ITokenService
	{
		string GenerateToken(string registrationNumber);
	}
}