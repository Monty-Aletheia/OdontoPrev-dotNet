using Aletheia.Domain.Entities;

namespace Aletheia.Domain.Interfaces
{
    public interface IClaimRepository : IRepository<Claim>
    {
        Task<Claim> GetClaimWithConsultationByIdAsync(Guid id);

        Task<IEnumerable<Claim>> GetClaimWithConsultationAsync();
    }
}
