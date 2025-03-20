using AutoMapper;
using ClaimService.Application.Dtos;
using ClaimService.Application.Services.HttpClients.Interface;
using ClaimService.Domain.Interfaces;
using ClaimService.Domain.Models;

namespace ClaimService.Application.Services
{
	public class ClaimAppService
	{
		private readonly IClaimRepository _repository;
		private readonly IMapper _mapper;
		private readonly IConsultationHttpClient _claimHttpClient;

		public ClaimAppService(IClaimRepository claimRepository,
						IMapper mapper,
						IConsultationHttpClient claimHttpClient)
		{
			_repository = claimRepository;
			_mapper = mapper;
			_claimHttpClient = claimHttpClient;
		}

		public async Task<IEnumerable<ClaimResponseDTO>> GetAllClaimsAsync()
		{
			var claims = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<ClaimResponseDTO>>(claims);
		}

		public async Task<ClaimResponseDTO> GetClaimByIdAsync(Guid id)
		{
			var claim = await _repository.GetByIdAsync(id);

			if (claim == null)
				throw new KeyNotFoundException($"Claim with id {id} not found.");

			return _mapper.Map<ClaimResponseDTO>(claim);
		}

		public async Task<ClaimResponseDTO> CreateClaimAsync(CreateClaimDTO dto)
		{
			var response = await _claimHttpClient.GetAsync($"{dto.ConsultationId}");
			Console.WriteLine(response);
			if (!response.IsSuccessStatusCode)
				throw new Exception($"Consultation with id {dto.ConsultationId} not found.");

			var claim = _mapper.Map<Claim>(dto);
			await _repository.AddAsync(claim);
			return _mapper.Map<ClaimResponseDTO>(claim);
		}

		public async Task<ClaimResponseDTO> UpdateClaimAsync(Guid id, UpdateClaimDTO dto)
		{
			var claim = await _repository.GetByIdAsync(id)
				?? throw new KeyNotFoundException($"Claim with id {id} not found.");

			if (dto.ConsultationId != null)
			{
				var response = await _claimHttpClient.GetAsync($"{dto.ConsultationId}");
				if (!response.IsSuccessStatusCode)
					throw new Exception($"Consultation with id {dto.ConsultationId} not found.");
			}

			_mapper.Map(dto, claim);
			await _repository.UpdateAsync(claim);
			return _mapper.Map<ClaimResponseDTO>(claim);
		}

		public async Task DeleteClaimAsync(Guid id)
		{
			var claim = await _repository.GetByIdAsync(id)
				?? throw new KeyNotFoundException($"Claim with id {id} not found.");

			await _repository.DeleteAsync(claim);
		}
	}
}