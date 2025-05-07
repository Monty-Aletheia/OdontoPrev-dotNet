using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.Common.Dtos
{
	public record PatientRiskAssessmentDTO
	{
		public Guid PatientID { get; set; }

		[Range(0, 120)]
		public float Age { get; init; }

		[Required]
		[EnumDataType(typeof(Gender))]
		public string Gender { get; init; }

		[Range(0, 365)]
		public float ConsultationFrequency { get; init; }

		[Range(0, 1)]
		public float AderenciaTratamento { get; init; } = 0; // 0.0 a 1.0

		[Required]
		public bool CavitiesHistory { get; init; }

		[Required]
		public bool PeriodontalDisease { get; init; }

		[Range(0, 100)]
		public float NumberOfImplants { get; init; }

		[Required]
		public bool PreviousComplexTreatments { get; init; }

		[Required]
		public bool Smoker { get; init; }

		[Required]
		public bool Alcoholism { get; init; }

		[Range(0, 10)]
		public float DailyBrushing { get; init; }

		[Required]
		public bool Flossing { get; init; }

		[Required]
		[EnumDataType(typeof(SystemicDiseases))]
		public string SystemicDiseases { get; init; }

		[Range(0, 1)]
		public float ContinuousMedicationUse { get; init; } = 0; // 0.0 ou 1.0

		[Range(0, float.MaxValue)]
		public float NumeroSinistrosPrevios { get; init; }

		[Required]
		[EnumDataType(typeof(PlanType))]
		public string PlanType { get; init; }
	}

}