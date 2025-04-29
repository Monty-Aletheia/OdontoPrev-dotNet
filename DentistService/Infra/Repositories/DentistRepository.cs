using DentistService.Domain.Interfaces;
using DentistService.Domain.Models;
using DentistService.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace DentistService.Infra.Repositories
{
	public class DentistRepository : Repository<Dentist>, IDentistRepository
	{
		private readonly FIAPDbContext _context;

		public DentistRepository(FIAPDbContext context) : base(context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context), "DbContext cannot be null");
		}

		public async Task<IEnumerable<Dentist>> GetDentistsByIds(IEnumerable<Guid> ids)
		{
			return await _context.Dentists
				.Where(d => ids.Contains(d.Id))
				.ToListAsync();
		}

		public async Task<Dentist> GetDentistsByRegistrationNumberAndPassword(string registrationNumber, string password)
		{
			return await _context.Dentists
				.FirstOrDefaultAsync(d => d.RegistrationNumber == registrationNumber && d.Password == password);
		}

	}
}