using Shared.Enums;

namespace ConsultationService.Application.Dtos
{
	public class ConsultationResponseDTO
	{
		public Guid Id { get; set; }

		public DateTime ConsultationDate { get; set; }

		public double? ConsultationValue { get; set; }

		public string RiskStatus { get; set; }

		public string? Description { get; set; }

		public PatientResponseDTO Patient { get; set; }

		public IEnumerable<DentistResponseDTO> Dentists { get; set; }
	}
}