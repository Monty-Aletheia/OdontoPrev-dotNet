using ConsultationService.Domain.Interfaces;
using ConsultationService.Domain.Models;
using ConsultationService.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService.Infra.Repositories
{
	public class ConsultationRepository : Repository<Consultation>, IConsultationRepository
	{
		private readonly FIAPDbContext _context;

		public ConsultationRepository(FIAPDbContext context) : base(context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context), "DbContext cannot be null");
		}

		// Método para adicionar um conjunto de entidades
		public async Task AddRangeAsync(IEnumerable<Consultation> entities)
		{
			await _context.Set<Consultation>().AddRangeAsync(entities); // Adiciona um conjunto de entidades
		}

		// Método para salvar as alterações no contexto
		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Consultation>> GetConsultationsWithDentistAsync()
		{
			var consultations = await _context.Set<Consultation>()
				.Include(c => c.ConsultationDentists)
				.ToListAsync();

			return consultations;
		}

		public async Task<Consultation> GetConsultationWithDentistsByIdAsync(Guid consultationId)
		{
			var consultation = await _context.Set<Consultation>()
				.Include(c => c.ConsultationDentists)
				.FirstOrDefaultAsync(c => c.Id == consultationId);

			return consultation;
		}

	}
}