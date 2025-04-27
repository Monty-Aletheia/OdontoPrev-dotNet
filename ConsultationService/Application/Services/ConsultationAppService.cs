using AutoMapper;
using ConsultationService.Application.Dtos;
using ConsultationService.Application.Services.HttpClients.Interfaces;
using ConsultationService.Application.Services.Interfaces;
using ConsultationService.Domain.Interfaces;
using ConsultationService.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConsultationService.Application.Services
{
	public class ConsultationAppService : IConsultationAppService
	{
		private readonly IConsultationRepository _consultationRepository;
		private readonly IMapper _mapper;
		private readonly IPatientHttpClient _patientHttpClient;
		private readonly IDentistHttpClient _dentistHttpClient;
		private readonly ILogger<ConsultationAppService> _logger;

		public ConsultationAppService(
			IConsultationRepository consultationRepository,
			IMapper mapper,
			IPatientHttpClient patientHttpClient,
			IDentistHttpClient dentistHttpClient,
			ILogger<ConsultationAppService> logger)
		{
			_consultationRepository = consultationRepository;
			_mapper = mapper;
			_patientHttpClient = patientHttpClient;
			_dentistHttpClient = dentistHttpClient;
			_logger = logger;
		}

		public async Task<IEnumerable<ConsultationResponseDTO>> GetConsultationsAsync()
		{
			return _mapper.Map<IEnumerable<ConsultationResponseDTO>>(await _consultationRepository.GetConsultationsWithDentistAsync());
		}

		public async Task<ConsultationResponseDTO> GetConsultationByIdAsync(Guid id)
		{
			var consultation = await _consultationRepository.GetConsultationWithDentistsByIdAsync(id);
			if (consultation == null)
			{
				_logger.LogWarning("Consultation with ID {ConsultationId} not found.", id);
				throw new KeyNotFoundException($"Consultation with id {id} not found.");
			}

			_logger.LogInformation("Consultation with ID {ConsultationId} found.", id);
			return _mapper.Map<ConsultationResponseDTO>(consultation);
		}

		public async Task<ConsultationResponseDTO> CreateConsultationAsync(CreateConsultationDTO dto)
		{
			try
			{
				if (!await ValidatePatient(dto.PatientId))
					throw new KeyNotFoundException($"Patient with id {dto.PatientId} not found.");

				foreach (var dentistId in dto.DentistIds)
				{
					if (!await ValidateDentist(dentistId))
						throw new KeyNotFoundException($"Dentist with id {dentistId} not found.");
				}

				var consultation = _mapper.Map<Consultation>(dto);
				consultation.ConsultationDentists = dto.DentistIds
					.Select(dentistId => new ConsultationDentist { DentistId = dentistId, Consultation = consultation })
					.ToList();

				await _consultationRepository.AddAsync(consultation);
				await _consultationRepository.SaveChangesAsync();

				_logger.LogInformation("Created consultation with ID {ConsultationId}", consultation.Id);

				return _mapper.Map<ConsultationResponseDTO>(consultation);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error creating consultation for patient {PatientId}", dto.PatientId);
				throw;
			}
		}

		public async Task<ConsultationResponseDTO> UpdateConsultationAsync(Guid id, UpdateConsultationDTO dto)
		{
			try
			{
				var consultation = await _consultationRepository.GetConsultationWithDentistsByIdAsync(id);
				if (consultation == null) throw new KeyNotFoundException($"Consultation with id {id} not found.");

				_mapper.Map(dto, consultation);
				await _consultationRepository.UpdateAsync(consultation);

				_logger.LogInformation("Updated consultation with ID {ConsultationId}", consultation.Id);

				return _mapper.Map<ConsultationResponseDTO>(consultation);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating consultation with ID {ConsultationId}", id);
				throw;
			}
		}

		public async Task DeleteConsultationAsync(Guid id)
		{
			try
			{
				var consultation = await _consultationRepository.GetByIdAsync(id);
				if (consultation == null) throw new KeyNotFoundException($"Consultation with id {id} not found.");

				await _consultationRepository.DeleteAsync(consultation);

				_logger.LogInformation("Deleted consultation with ID {ConsultationId}", id);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting consultation with ID {ConsultationId}", id);
				throw;
			}
		}

		public async Task<bool> ValidatePatient(Guid patientId)
		{
			var response = await _patientHttpClient.GetAsync($"{patientId}");
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> ValidateDentist(Guid dentistId)
		{
			var response = await _dentistHttpClient.GetAsync($"{dentistId}");
			return response.IsSuccessStatusCode;
		}
	}
}
