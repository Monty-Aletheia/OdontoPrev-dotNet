using Shared.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultationService.Domain.Models
{
    [Table("tb_consultations")]
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
    }
}
