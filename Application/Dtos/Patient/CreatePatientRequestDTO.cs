using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Aletheia.Application.Dtos.Patient
{
    public class CreatePatientRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public RiskStatus RiskStatus { get; set; }

        public int? ConsultationFrequency { get; set; } = 0;

        public string AssociatedClaims { get; set; }
    }
}
