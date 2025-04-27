using AutoMapper;
using DentistService.Application.Dtos;
using DentistService.Application.Services.Interfaces;
using DentistService.Domain.Interfaces;
using DentistService.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DentistService.Application.Services
{
	public class DentistAppService : IDentistAppService
	{
		private readonly IDentistRepository _dentistRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<DentistAppService> _logger;

		public DentistAppService(
			IDentistRepository dentistRepository,
			IMapper mapper,
			ILogger<DentistAppService> logger)
		{
			_dentistRepository = dentistRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<IEnumerable<DentistResponseDTO>> GetAllDentistsAsync()
		{
			var dentists = await _dentistRepository.GetAllAsync();
			_logger.LogInformation("Fetched {DentistCount} dentists.", dentists.Count());
			return _mapper.Map<IEnumerable<DentistResponseDTO>>(dentists);
		}

		public async Task<DentistResponseDTO> GetDentistByIdAsync(Guid id)
		{
			var dentist = await _dentistRepository.GetByIdAsync(id);

			if (dentist == null)
			{
				_logger.LogWarning("Dentist with ID {DentistId} not found.", id);
				throw new KeyNotFoundException($"Dentist with id {id} not found.");
			}

			return _mapper.Map<DentistResponseDTO>(dentist);
		}

		public async Task<DentistResponseDTO?> GetDentistByRegistrationNumberAndPassword(LoginDTO dto)
		{
			var dentist = await _dentistRepository.GetByRegistrationNumberAndPassword(dto.RegistrationNumber, dto.Password);

			if (dentist == null)
			{
				_logger.LogWarning("Invalid login attempt for RegistrationNumber {RegistrationNumber}.", dto.RegistrationNumber);
				return null;
			}

			return _mapper.Map<DentistResponseDTO>(dentist);
		}

		public async Task<DentistResponseDTO> CreateDentistAsync(CreateDentistDTO dto)
		{
			var dentist = _mapper.Map<Dentist>(dto);
			await _dentistRepository.AddAsync(dentist);

			_logger.LogInformation("Created dentist with ID {DentistId}.", dentist.Id);
			return _mapper.Map<DentistResponseDTO>(dentist);
		}

		public async Task<DentistResponseDTO> UpdateDentistAsync(Guid id, UpdateDentistDTO dto)
		{
			var dentist = await _dentistRepository.GetByIdAsync(id);

			if (dentist == null)
			{
				_logger.LogWarning("Cannot update: Dentist with ID {DentistId} not found.", id);
				throw new KeyNotFoundException($"Dentist with id {id} not found.");
			}

			_mapper.Map(dto, dentist);
			await _dentistRepository.UpdateAsync(dentist);

			_logger.LogInformation("Updated dentist with ID {DentistId}.", id);
			return _mapper.Map<DentistResponseDTO>(dentist);
		}

		public async Task DeleteDentistAsync(Guid id)
		{
			var dentist = await _dentistRepository.GetByIdAsync(id);

			if (dentist == null)
			{
				_logger.LogWarning("Cannot delete: Dentist with ID {DentistId} not found.", id);
				throw new KeyNotFoundException($"Dentist with id {id} not found.");
			}

			await _dentistRepository.DeleteAsync(dentist);

			_logger.LogInformation("Deleted dentist with ID {DentistId}.", id);
		}
	}
}
