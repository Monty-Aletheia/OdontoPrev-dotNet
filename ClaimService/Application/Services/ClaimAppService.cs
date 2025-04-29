using AutoMapper;
using ClaimService.Application.Dtos;
using ClaimService.Application.Services.HttpClients.Interfaces;
using ClaimService.Application.Services.Interfaces;
using ClaimService.Domain.Interfaces;
using ClaimService.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ClaimService.Application.Services
{
	public class ClaimAppService : IClaimAppService
	{
		private readonly IClaimRepository _repository;
		private readonly IMapper _mapper;
		private readonly IConsultationHttpClient _claimHttpClient;
		private readonly ILogger<ClaimAppService> _logger;

		public ClaimAppService(
			IClaimRepository repository,
			IMapper mapper,
			IConsultationHttpClient claimHttpClient,
			ILogger<ClaimAppService> logger)
		{
			_repository = repository;
			_mapper = mapper;
			_claimHttpClient = claimHttpClient;
			_logger = logger;
		}

		public async Task<IEnumerable<ClaimResponseDTO>> GetAllClaimsAsync()
		{
			var claims = await _repository.GetAllAsync();

			_logger.LogInformation("Retrieved {Count} claims.", claims.Count());
			return _mapper.Map<IEnumerable<ClaimResponseDTO>>(claims);
		}

		public async Task<ClaimResponseDTO> GetClaimByIdAsync(Guid id)
		{
			var claim = await _repository.GetByIdAsync(id);

			if (claim == null)
			{
				_logger.LogWarning("Claim with ID {ClaimId} not found.", id);
				throw new KeyNotFoundException($"Claim with id {id} not found.");
			}

			_logger.LogInformation("Claim with ID {ClaimId} found.", id);
			return _mapper.Map<ClaimResponseDTO>(claim);
		}

		public async Task<ClaimResponseDTO> CreateClaimAsync(CreateClaimDTO dto)
		{
			var response = await _claimHttpClient.GetAsync($"{dto.ConsultationId}");

			if (!response.IsSuccessStatusCode)
			{
				_logger.LogWarning("Consultation with ID {ConsultationId} not found.", dto.ConsultationId);
				throw new KeyNotFoundException($"Consultation with id {dto.ConsultationId} not found.");
			}

			var claim = _mapper.Map<Claim>(dto);
			await _repository.AddAsync(claim);

			_logger.LogInformation("Claim with ID {ClaimId} created successfully.", claim.Id);
			return _mapper.Map<ClaimResponseDTO>(claim);
		}

		public async Task<ClaimResponseDTO> UpdateClaimAsync(Guid id, UpdateClaimDTO dto)
		{
			var claim = await _repository.GetByIdAsync(id);

			if (claim == null)
			{
				_logger.LogWarning("Claim with ID {ClaimId} not found for update.", id);
				throw new KeyNotFoundException($"Claim with id {id} not found.");
			}

			_mapper.Map(dto, claim);
			await _repository.UpdateAsync(claim);

			_logger.LogInformation("Claim with ID {ClaimId} updated successfully.", id);
			return _mapper.Map<ClaimResponseDTO>(claim);
		}

		public async Task DeleteClaimAsync(Guid id)
		{
			var claim = await _repository.GetByIdAsync(id);

			if (claim == null)
			{
				_logger.LogWarning("Claim with ID {ClaimId} not found for deletion.", id);
				throw new KeyNotFoundException($"Claim with id {id} not found.");
			}

			await _repository.DeleteAsync(claim);

			_logger.LogInformation("Claim with ID {ClaimId} deleted successfully.", id);
		}
	}
}