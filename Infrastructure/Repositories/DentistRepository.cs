using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Repositories
{
    public class DentistRepository(DbContext context) : Repository<Dentist>(context), IDentistRepository
    {
    }
}
