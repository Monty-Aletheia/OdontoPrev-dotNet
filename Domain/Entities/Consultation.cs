using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aletheia.Domain.Entities
{
    public class Consultation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column("consultation_date")]
        public DateTime ConsultationDate { get; set; }

        [Required]
        [Column("consultation_value")]
        public double ConsultationValue { get; set; }

        [Required]
        [Column("risk_status")]
        public RiskStatus RiskStatus { get; set; }

        public string Description { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual ICollection<Dentist> Dentists { get; set; }

        public virtual ICollection<Claim> Claims { get; set; }
    }
}
