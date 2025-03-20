using Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace ConsultationService.Application.Dtos
{
	public class UpdateConsultationDTO
	{
		public DateTime? ConsultationDate { get; set; }

		[Range(0.01, double.MaxValue)]
		public double? ConsultationValue { get; set; }

		public RiskStatus? RiskStatus { get; set; }

		[StringLength(500)]
		public string? Description { get; set; }
	}
}