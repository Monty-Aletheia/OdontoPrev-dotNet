using PatientService.Application.Dtos;
using Shared.Common.Dtos;

namespace PatientService.Application.Services.Interfaces
{
	public interface IPatientAppService
	{
		Task<IEnumerable<PatientResponseDTO>> GetAllPatientsAsync();
		Task<PatientResponseDTO> GetPatientByIdAsync(Guid id);
		Task<PatientResponseDTO> CreatePatientAsync(CreatePatientDTO dto);
		Task<PatientResponseDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto);
		Task<bool> DeletePatientAsync(Guid id);
		Task RequestPredictionAsync(PatientRiskAssessmentDTO dto);
		Task SavePredictionResultAsync(PredictionResultDTO result);
	}
}