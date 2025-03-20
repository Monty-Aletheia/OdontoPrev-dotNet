using PatientService.Domain.Models;
using Shared.Interfaces;

namespace PatientService.Domain.Interfaces
{
	public interface IPatientRepository : IRepository<Patient>
	{
	}
}