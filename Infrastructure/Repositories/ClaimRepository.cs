using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Repositories
{
    public class ClaimRepository : Repository<Claim>, IClaimRepository
    {
        private readonly FIAPDbContext _context;

        public ClaimRepository(FIAPDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Claim>> GetClaimWithConsultationAsync()
        {
            return await _context.Claims
                       .Include(c => c.Consultation).ToListAsync();
            ;
        }

        public async Task<Claim> GetClaimWithConsultationByIdAsync(Guid id)
        {
            return await _context.Claims
                .Include(c => c.Consultation)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
