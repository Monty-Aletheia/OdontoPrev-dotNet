using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Aletheia.Application.Dtos.Dentist
{
    public class CreateDentistRequestDTO
    {
        [Required]
        public required string Name { get; set; }

        public string Specialty { get; set; }

        [Required]
        public required string RegistrationNumber { get; set; }

        public double? ClaimsRate { get; set; }

        [Required]
        public RiskStatus RiskStatus { get; set; }
    }
}
