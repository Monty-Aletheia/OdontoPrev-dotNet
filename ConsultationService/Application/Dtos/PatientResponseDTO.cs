using Shared.Enums;

namespace ConsultationService.Application.Dtos
{
	public class PatientResponseDTO
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public DateTime Birthday { get; set; }

		public Gender Gender { get; set; }

		public RiskStatus RiskStatus { get; set; }

		public int? ConsultationFrequency { get; set; } = 0;

		public string AssociatedClaims { get; set; }

		public override string ToString()
		{
			return $"Id: {Id}, Name: {Name}, Birthday: {Birthday}, Gender: {Gender}, RiskStatus: {RiskStatus}, ConsultationFrequency: {ConsultationFrequency}, AssociatedClaims: {AssociatedClaims}";
		}
	}


}