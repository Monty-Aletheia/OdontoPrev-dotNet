
using Shared.Enum;

namespace ClaimService.Application.Dtos
{
	public class ClaimResponseDTO
	{
		public Guid Id { get; set; }

		public DateTime OccurrenceDate { get; set; }

		public double? Value { get; set; }

		public ClaimType ClaimType { get; set; }

		public string SuggestedPreventiveAction { get; set; }

	}
}