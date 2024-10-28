using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Data;

namespace Aletheia.Infra.Repositories
{
    public class DentistRepository(FIAPDbContext context) : Repository<Dentist>(context), IDentistRepository
    {
    }
}
