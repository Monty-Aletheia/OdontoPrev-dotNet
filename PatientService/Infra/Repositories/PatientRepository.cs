using PatientService.Domain.Interfaces;
using PatientService.Domain.Models;
using PatientService.Infra.Data;

namespace PatientService.Infra.Repositories
{
	public class PatientRepository(FIAPDbContext context) : Repository<Patient>(context), IPatientRepository
	{
	}
}