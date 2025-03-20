using DentistService.Domain.Interfaces;
using DentistService.Domain.Models;
using DentistService.Infra.Data;

namespace DentistService.Infra.Repositories
{
	public class DentistRepository(FIAPDbContext context) : Repository<Dentist>(context), IDentistRepository
	{
	}
}