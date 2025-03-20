using Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace PatientService.Application.Dtos
{
	public class CreatePatientDTO
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public DateTime Birthday { get; set; }

		[Required]
		public Gender Gender { get; set; }

		[Required]
		public RiskStatus RiskStatus { get; set; }

		public int? ConsultationFrequency { get; set; } = 0;

		public string AssociatedClaims { get; set; }
	}
}