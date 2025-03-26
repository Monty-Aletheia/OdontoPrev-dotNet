using Shared.Enums;

namespace PatientService.Application.Dtos
{
	public class UpdatePatientDTO
	{
		public string? Name { get; set; }

		public DateTime? Birthday { get; set; }

		public Gender? Gender { get; set; }

		public RiskStatus? RiskStatus { get; set; }

		public int? ConsultationFrequency { get; set; }

		public string? AssociatedClaims { get; set; }
	}
}