using Aletheia.Application.Dtos.Claim;
using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using AutoMapper;

namespace Aletheia.Application.Services
{
    public class ClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IConsultationRepository _consultationRepository;
        private readonly IMapper _mapper;

        public ClaimService(IClaimRepository claimRepository,
                            IConsultationRepository consultationRepository,
                            IMapper mapper)
        {
            _claimRepository = claimRepository;
            _consultationRepository = consultationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClaimResponseDTO>> GetAllClaimsAsync()
        {
            var claims = await _claimRepository.GetClaimWithConsultationAsync();
            return _mapper.Map<IEnumerable<ClaimResponseDTO>>(claims);
        }

        public async Task<ClaimResponseDTO> GetClaimByIdAsync(Guid id)
        {
            var claim = await _claimRepository.GetClaimWithConsultationByIdAsync(id);

            if (claim == null)
                throw new KeyNotFoundException($"Claim with id {id} not found.");

            return _mapper.Map<ClaimResponseDTO>(claim);
        }

        public async Task<ClaimResponseDTO> CreateClaimAsync(CreateClaimDTO dto)
        {
            var consultation = await _consultationRepository.GetByIdAsync(dto.ConsultationId)
                ?? throw new KeyNotFoundException($"Consultation with id {dto.ConsultationId} not found.");

            var claim = _mapper.Map<Claim>(dto);
            claim.Consultation = consultation;

            await _claimRepository.AddAsync(claim);

            return _mapper.Map<ClaimResponseDTO>(claim);
        }

        public async Task<ClaimResponseDTO> UpdateClaimAsync(Guid id, UpdateClaimDTO dto)
        {
            var claim = await _claimRepository.GetClaimWithConsultationByIdAsync(id)
                ?? throw new KeyNotFoundException($"Claim with id {id} not found.");

            if (dto.ConsultationId != null)
            {
                var consultation = await _consultationRepository.GetByIdAsync(dto.ConsultationId.Value)
                    ?? throw new KeyNotFoundException($"Consultation with id {dto.ConsultationId} not found.");
                claim.Consultation = consultation;
            }

            _mapper.Map(dto, claim);
            await _claimRepository.UpdateAsync(claim);

            return _mapper.Map<ClaimResponseDTO>(claim);
        }

        public async Task DeleteClaimAsync(Guid id)
        {
            var claim = await _claimRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Claim with id {id} not found.");

            await _claimRepository.DeleteAsync(claim);
        }
    }
}
