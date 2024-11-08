using Aletheia.Domain.Entities.Enum;

namespace Aletheia.Application.Dtos.Dentist
{
    public class UpdateDentistDTO
    {
        public string? Name { get; set; }

        public string? Specialty { get; set; }

        public string? RegistrationNumber { get; set; }

        public double? ClaimsRate { get; set; }

        public RiskStatus? RiskStatus { get; set; }
    }
}
