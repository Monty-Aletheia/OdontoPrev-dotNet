using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClaimService.Application.Dtos
{
	public class UpdateClaimDTO
	{
		public DateTime? OccurrenceDate { get; set; }

		[Range(0.01, double.MaxValue)]
		public double? Value { get; set; }

		[EnumDataType(typeof(ClaimType))]
		public string? ClaimType { get; set; }

		[StringLength(255)]
		public string? SuggestedPreventiveAction { get; set; }
	}
}