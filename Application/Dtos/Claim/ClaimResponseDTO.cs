using Aletheia.Domain.Entities.Enum;

namespace Aletheia.Application.Dtos.Claim
{
    public class ClaimResponseDTO
    {
        public Guid Id { get; set; }

        public DateTime OccurrenceDate { get; set; } 

        public double? Value { get; set; } 

        public ClaimType ClaimType { get; set; } 

        public string SuggestedPreventiveAction { get; set; } 

        public Guid ConsultationId { get; set; }
    }
}
