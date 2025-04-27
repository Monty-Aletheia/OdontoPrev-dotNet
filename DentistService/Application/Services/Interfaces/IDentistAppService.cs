using DentistService.Application.Dtos;

namespace DentistService.Application.Services.Interfaces
{
	public interface IDentistAppService
	{
		Task<IEnumerable<DentistResponseDTO>> GetAllDentistsAsync();
		Task<DentistResponseDTO> GetDentistByIdAsync(Guid id);
		Task<DentistResponseDTO?> GetDentistByRegistrationNumberAndPassword(LoginDTO dto);
		Task<DentistResponseDTO> CreateDentistAsync(CreateDentistDTO dto);
		Task<DentistResponseDTO> UpdateDentistAsync(Guid id, UpdateDentistDTO dto);
		Task DeleteDentistAsync(Guid id);
	}

}
