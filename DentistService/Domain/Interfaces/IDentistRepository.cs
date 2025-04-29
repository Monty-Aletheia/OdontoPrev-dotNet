using DentistService.Domain.Models;
using Shared.Interfaces;

namespace DentistService.Domain.Interfaces
{
	public interface IDentistRepository : IRepository<Dentist>
	{

		Task<Dentist> GetDentistsByRegistrationNumberAndPassword(string registrationNumber, string password);

		Task<IEnumerable<Dentist>> GetDentistsByIds(IEnumerable<Guid> ids);
	}
}