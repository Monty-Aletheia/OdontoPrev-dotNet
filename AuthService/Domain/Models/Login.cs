using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Models
{
	public class Login
	{

		[Required]
		public string RegistrationNumber { get; set; }

		[Required]
		public required string Password { get; set; }

	}
}
