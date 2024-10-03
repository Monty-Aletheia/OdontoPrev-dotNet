using Aletheia.Domain.Entities;
using Aletheia.Domain.IRepository;
using Aletheia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Repositories
{
    public class ClaimRepository(DbContext context) : Repository<Claim>(context), IClaimRepository
    {
    }
}
