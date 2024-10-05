using Aletheia.Application.Dtos.Dentist;
using Aletheia.Application.Dtos.Patient;
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

        public PatientResponseDTO Patient { get; set; }

        public IEnumerable<DentistResponseDTO> Dentists { get; set; }
    }
}
