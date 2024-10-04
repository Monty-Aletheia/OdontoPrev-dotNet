using Aletheia.Domain.Entities.Enum;

namespace Aletheia.Application.Dtos.Consultation
{
    public class ConsultationResponseDTO
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public double? ConsultationValue { get; set; } 

        public RiskStatus RiskStatus { get; set; }

        public string? Description { get; set; } 
    }
}
