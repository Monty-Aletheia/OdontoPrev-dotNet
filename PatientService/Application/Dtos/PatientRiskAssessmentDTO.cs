namespace PatientService.Application.Dtos
{
	using PatientService.Domain.Enums;
	using System.ComponentModel.DataAnnotations;

	public record PatientRiskAssessmentDTO
	{
		[Range(0, int.MaxValue)] 
		public int Age { get; init; }

		[Required] 
		public string Gender { get; init; }

		[Range(0, int.MaxValue)]
		public int ConsultationFrequency { get; init; }

		[Range(0, int.MaxValue)]
		public int CavitiesHistory { get; init; }

		[Range(0, int.MaxValue)]
		public int PeriodontalDisease { get; init; }

		[Range(0, int.MaxValue)]
		public int NumberOfImplants { get; init; }

		[Range(0, int.MaxValue)]
		public int PreviousComplexTreatments { get; init; }

		[Range(0, 1)] 
		public bool Smoker { get; init; }

		[Range(0, 1)]
		public bool Alcoholism { get; init; }

		[Range(0, int.MaxValue)]
		public int DailyBrushing { get; init; }

		[Range(0, 1)]
		public bool Flossing { get; init; }

		public SystemicDiseases SystemicDiseases { get; init; }

		[Range(0, 1)]
		public bool ContinuousMedicationUse { get; init; }

		public PlanType PlanType { get; init; }
	}

}
