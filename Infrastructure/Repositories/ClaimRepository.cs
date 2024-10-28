using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Data;

namespace Aletheia.Infra.Repositories
{
    public class ClaimRepository(FIAPDbContext context) : Repository<Claim>(context), IClaimRepository
    {
    }
}
