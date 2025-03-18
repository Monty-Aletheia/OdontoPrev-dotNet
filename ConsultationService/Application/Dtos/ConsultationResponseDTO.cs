using Shared.Enum;

namespace ConsultationService.Application.Dtos
{
    public class ConsultationResponseDTO
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public double? ConsultationValue { get; set; }

        public RiskStatus RiskStatus { get; set; }

        public string? Description { get; set; }

        public Guid Patient { get; set; }

        public IEnumerable<Guid> Dentists { get; set; }
    }
}
