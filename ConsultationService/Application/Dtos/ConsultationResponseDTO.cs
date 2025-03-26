using Shared.Enums;

namespace ConsultationService.Application.Dtos
{
	public class ConsultationResponseDTO
	{
		public Guid Id { get; set; }

		public DateTime ConsultationDate { get; set; }

		public double? ConsultationValue { get; set; }

		public RiskStatus RiskStatus { get; set; }

		public string? Description { get; set; }

		public Guid PatientId { get; set; }

		public ICollection<Guid> DentistIds { get; set; } = new List<Guid>();
	}
}