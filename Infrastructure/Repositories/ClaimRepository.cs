using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Repositories
{
    public class ClaimRepository(DbContext context) : Repository<Claim>(context), IClaimRepository
    {
    }
}
