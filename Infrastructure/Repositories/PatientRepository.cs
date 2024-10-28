using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Data;

namespace Aletheia.Infra.Repositories
{
    public class PatientRepository(FIAPDbContext context) : Repository<Patient>(context), IPatientRepository
    {
    }
}
