
using ClaimService.Domain.Interfaces;
using ClaimService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimService.Infra.Repositories
{
	public class ClaimRepository : Repository<Claim>, IClaimRepository
	{
		private readonly FIAPDbContext _context;

		public ClaimRepository(FIAPDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Claim>> GetAllClaimsFromConsultationIdAsync(Guid consultationId)
		{
			return await _context.Claims
				.Where(c => c.ConsultationId == consultationId)
				.ToListAsync();
		}
	}
}