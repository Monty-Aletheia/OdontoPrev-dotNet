using DentistService.Domain.Models;
using Shared.Interfaces;

namespace DentistService.Domain.Interfaces
{
	public interface IDentistRepository : IRepository<Dentist>
	{

		Task<Dentist> GetByRegistrationNumberAndPassword(string registrationNumber, string password);
	}
}