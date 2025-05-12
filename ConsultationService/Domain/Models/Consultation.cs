using Shared.Enums;
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

		public string? Description { get; set; }

		[Required]
		[Column("patient_id")]
		public Guid PatientId { get; set; }
		public ICollection<ConsultationDentist> ConsultationDentists { get; set; }
	}

	// Tabela de junção para Consulta-Dentista (n:n)
	[Table("tb_consultation_dentists")]
	public class ConsultationDentist
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[ForeignKey("Consultation")]
		public Guid ConsultationId { get; set; }

		[Required]
		[Column("dentist_id")]
		public Guid DentistId { get; set; }

		public Consultation Consultation { get; set; }
	}
}