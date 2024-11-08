using Aletheia.Domain.Entities;

namespace Aletheia.Domain.Interfaces
{
    public interface IConsultationRepository : IRepository<Consultation>
    {
        Task<Consultation> GetConsultationWithPatientAndDentistsByIdAsync(Guid id);

        Task<IEnumerable<Consultation>> GetConsultationWithPatientAndDentistsAsync();
    }
}
