using ConsultationService.Application.Dtos;

namespace ConsultationService.Application.Services.Interfaces
{
	public interface IConsultationAppService
	{
		Task<IEnumerable<ConsultationResponseDTO>> GetConsultationsAsync();
		Task<ConsultationResponseDTO> GetConsultationByIdAsync(Guid id);
		Task<ConsultationResponseDTO> CreateConsultationAsync(CreateConsultationDTO dto);
		Task<ConsultationResponseDTO> UpdateConsultationAsync(Guid id, UpdateConsultationDTO dto);
		Task DeleteConsultationAsync(Guid id);
		Task<bool> ValidatePatient(Guid patientId);
		Task<bool> ValidateDentist(Guid dentistId);
	}
	
}
