using Shared.Enums;
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
		[EnumDataType(typeof(Gender))]
		public string Gender { get; set; }

		[Required]
		[EnumDataType(typeof(RiskStatus))]
		public RiskStatus RiskStatus { get; set; }

		public int? ConsultationFrequency { get; set; } = 0;

		public string AssociatedClaims { get; set; }
	}
}