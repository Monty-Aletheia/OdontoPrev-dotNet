using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Aletheia.Application.Dtos.Consultation
{
    public class CreateConsultationDTO
    {
        [Required]
        public DateTime ConsultationDate { get; set; } 

        [Required]
        public double? ConsultationValue { get; set; } 

        [Required]
        public RiskStatus RiskStatus { get; set; } 

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public required List<Guid> DentistIds { get; set; } 

        public string? Description { get; set; } 

    }
}
