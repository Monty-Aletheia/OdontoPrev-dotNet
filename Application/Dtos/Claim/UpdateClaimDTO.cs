using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Aletheia.Application.Dtos.Claim
{
    public class UpdateClaimDTO
    {
        public DateTime? OccurrenceDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public double? Value { get; set; }

        public ClaimType? ClaimType { get; set; }

        [StringLength(255)]
        public string? SuggestedPreventiveAction { get; set; }

        public Guid? ConsultationId { get; set; }
    }
}
