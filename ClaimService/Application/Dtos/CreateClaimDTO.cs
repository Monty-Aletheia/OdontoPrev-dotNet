using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClaimService.Application.Dtos
{
	public class CreateClaimDTO
	{
		[Required]
		public DateTime OccurrenceDate { get; set; }

		[Required]
		[Range(0.01, double.MaxValue)]
		public double Value { get; set; }

		[Required]
		[EnumDataType(typeof(ClaimType))]
		public string ClaimType { get; set; }

		[StringLength(255)]
		public string SuggestedPreventiveAction { get; set; }

		[Required]
		public Guid ConsultationId { get; set; }
	}
}