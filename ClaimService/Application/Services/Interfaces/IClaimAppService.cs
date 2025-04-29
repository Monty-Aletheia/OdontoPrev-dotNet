using ClaimService.Application.Dtos;

namespace ClaimService.Application.Services.Interfaces
{
	public interface IClaimAppService
	{
		Task<IEnumerable<ClaimResponseDTO>> GetAllClaimsAsync();
		Task<ClaimResponseDTO> GetClaimByIdAsync(Guid id);
		Task<ClaimResponseDTO> CreateClaimAsync(CreateClaimDTO dto);
		Task<ClaimResponseDTO> UpdateClaimAsync(Guid id, UpdateClaimDTO dto);
		Task DeleteClaimAsync(Guid id);
	}
}