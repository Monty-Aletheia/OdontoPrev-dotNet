using ConsultationService.Domain.Models;
using Shared.Interfaces;

namespace ConsultationService.Domain.Interfaces
{
    public interface IConsultationRepository : IRepository<Consultation>
    {
        Task<Consultation> GetConsultationWithPatientAndDentistsByIdAsync(Guid id);

        Task<IEnumerable<Consultation>> GetConsultationWithPatientAndDentistsAsync();
    }
}
