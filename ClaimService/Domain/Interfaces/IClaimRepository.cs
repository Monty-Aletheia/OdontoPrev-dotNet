
using ClaimService.Domain.Models;
using Shared.Interfaces;

namespace ClaimService.Domain.Interfaces
{
	public interface IClaimRepository : IRepository<Claim>
	{
		Task<IEnumerable<Claim>> GetAllClaimsFromConsultationIdAsync(Guid consultationId);
	}
}