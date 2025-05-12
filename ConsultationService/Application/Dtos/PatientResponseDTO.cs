using Shared.Enums;

namespace ConsultationService.Application.Dtos
{
	public class PatientResponseDTO
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public DateTime Birthday { get; set; }

		public string Gender { get; set; }

		public string RiskStatus { get; set; }

		public int? ConsultationFrequency { get; set; } = 0;

		public string AssociatedClaims { get; set; }
	}


}