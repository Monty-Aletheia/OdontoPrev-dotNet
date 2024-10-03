using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aletheia.Domain.Entities
{

    public class Dentist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Specialty { get; set; }

        [Required]
        [Column("registration_number")]
        public string RegistrationNumber { get; set; }

        [Column("claims_rate")]
        public double? ClaimsRate { get; set; }

        [Required]
        [Column("risk_status")]
        public RiskStatus RiskStatus { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
