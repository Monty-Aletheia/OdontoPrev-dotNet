using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using Aletheia.Infra.Data;

namespace Aletheia.Infra.Repositories
{
    public class ConsultationRepository(FIAPDbContext context) : Repository<Consultation>(context), IConsultationRepository
    {
    }
}
