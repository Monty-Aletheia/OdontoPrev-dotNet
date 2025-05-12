using System.ComponentModel.DataAnnotations;

namespace DentistService.Application.Dtos
{
	public class LoginDTO
	{

		[Required]
		public string RegistrationNumber { get; set; }

		[Required]
		public required string Password { get; set; }

	}
}