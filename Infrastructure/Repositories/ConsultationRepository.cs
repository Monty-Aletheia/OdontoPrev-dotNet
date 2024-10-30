using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Aletheia.Infra.Repositories
{
    public class ConsultationRepository : Repository<Consultation>, IConsultationRepository
    {
        private readonly FIAPDbContext _context;

        // Construtor corrigido
        public ConsultationRepository(FIAPDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "DbContext cannot be null");
        }

        public async Task<Consultation> GetConsultationWithPatientAndDentistsByIdAsync(Guid id)
        {
            return await _context.Set<Consultation>()
                .Include(c => c.Patient)
                .Include(c => c.Dentists)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Consultation>> GetConsultationWithPatientAndDentistsAsync()
        {
            return await _context.Set<Consultation>()
                .Include(c => c.Patient)
                .Include(c => c.Dentists)
                .ToListAsync();
        }
    }
}
