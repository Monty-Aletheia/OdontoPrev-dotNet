using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Models
{
	public class Signup
	{
		[Required]
		public required string Name { get; set; }

		public string Specialty { get; set; }

		[Required]
		public required string RegistrationNumber { get; set; }

		public double? ClaimsRate { get; set; }

		[Required]
		public RiskStatus RiskStatus { get; set; }
	}
}
