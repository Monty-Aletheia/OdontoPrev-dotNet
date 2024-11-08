using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Aletheia.Application.Dtos.Consultation
{
    public class UpdateConsultationDTO
    {
        public DateTime? ConsultationDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public double? ConsultationValue { get; set; }

        public RiskStatus? RiskStatus { get; set; }

        public Guid? PatientId { get; set; }

        public List<Guid>? DentistIds { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
