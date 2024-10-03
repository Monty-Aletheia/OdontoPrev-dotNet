using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aletheia.Domain.Entities
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [Column("risk_status")]
        public RiskStatus RiskStatus { get; set; }

        [Column("consultation_frequency", TypeName = "int")]
        public int? ConsultationFrequency { get; set; } = 0;

        [Column("associated_claims", TypeName = "text")]
        public string AssociatedClaims { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
