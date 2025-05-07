using MlNetWorker.Models;
using Shared.Common.Dtos;

namespace MlNetWorker.Services
{
	public static class PatientRiskAssessmentMapper
	{
		public static InputData ToInputData(PatientRiskAssessmentDTO dto)
		{
			return new InputData
			{
				Idade = dto.Age,
				Genero = dto.Gender,
				FrequenciaConsultas = dto.ConsultationFrequency,
				AderenciaTratamento = dto.AderenciaTratamento,
				HistoricoCaries = dto.CavitiesHistory,
				DoencaPeriodontal = dto.PeriodontalDisease,
				NumeroImplantes = dto.NumberOfImplants,
				TratamentosComplexosPrevios = dto.PreviousComplexTreatments,
				Fumante = dto.Smoker,
				Alcoolismo = dto.Alcoholism,
				EscovacaoDiaria = dto.DailyBrushing,
				UsoFioDental = dto.Flossing,
				DoencasSistemicas = dto.SystemicDiseases,
				MedicamentosUsoContinuo = dto.ContinuousMedicationUse,
				NumeroSinistrosPrevios = dto.NumeroSinistrosPrevios,
				TipoPlano = dto.PlanType
			};
		}
	}

}
