using ConsultationService.Domain.Interfaces;
using ConsultationService.Domain.Models;
using ConsultationService.Infra.Data;
using DentistService.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConsultationService.Infra.Repositories
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
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Consultation>> GetConsultationWithPatientAndDentistsAsync()
        {
            return await _context.Set<Consultation>()
                .ToListAsync();
        }
    }
}
