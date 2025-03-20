
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

	}
}