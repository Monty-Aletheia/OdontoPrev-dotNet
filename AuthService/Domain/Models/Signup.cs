using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Models
{
	public class Signup
	{
		[Required]
		public string Name { get; set; }

		public string Specialty { get; set; }

		[Required]
		public string RegistrationNumber { get; set; }

		public double? ClaimsRate { get; set; }

		public string Password { get; set; }

		[Required]
		[EnumDataType(typeof(RiskStatus))]
		public string RiskStatus { get; set; }
	}
}