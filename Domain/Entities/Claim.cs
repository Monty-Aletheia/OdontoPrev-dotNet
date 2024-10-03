using Aletheia.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aletheia.Domain.Entities
{
    [Table("tb_claims")]
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Column("occurrence_date")]
        public DateTime OccurrenceDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }

        [Required]
        [Column("claim_type")]
        public ClaimType ClaimType { get; set; }

        [Column("suggested_preventive_action")]
        public string SuggestedPreventiveAction { get; set; }

        [Required]
        [ForeignKey("Consultation")]
        public Guid ConsultationId { get; set; }
        public virtual Consultation Consultation { get; set; }
    }
}
