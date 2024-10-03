using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Aletheia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Repositories
{
    public class PatientRepository(DbContext context) : Repository<Patient>(context), IPatientRepository
    {
    }
}
