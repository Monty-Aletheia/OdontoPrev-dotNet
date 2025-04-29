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
				var consultations = await _consultationRepository.GetConsultationsWithDentistAsync();

				var consultationResponseDtos = _mapper.Map<IEnumerable<ConsultationResponseDTO>>(consultations);

				foreach (var consultationResponse in consultationResponseDtos)
				{
					await PopulateConsultationDetails(consultationResponse, consultations);
				}

				return consultationResponseDtos;
			}

			public async Task<ConsultationResponseDTO> GetConsultationByIdAsync(Guid id)
			{
				var consultation = await _consultationRepository.GetConsultationWithDentistsByIdAsync(id);
				if (consultation == null)
				{
					_logger.LogWarning("Consultation with ID {ConsultationId} not found.", id);
					throw new KeyNotFoundException($"Consultation with id {id} not found.");
				}

				var consultationResponse = _mapper.Map<ConsultationResponseDTO>(consultation);

				await PopulateConsultationDetails(consultationResponse, new[] { consultation });

				_logger.LogInformation("Consultation with ID {ConsultationId} found.", id);

				return consultationResponse;
			}

			public async Task<ConsultationResponseDTO> CreateConsultationAsync(CreateConsultationDTO dto)
			{
				try
				{
					await ValidateEntitiesAsync(dto.PatientId, dto.DentistIds);

					var consultation = _mapper.Map<Consultation>(dto);
					consultation.ConsultationDentists = CreateConsultationDentists(dto.DentistIds, consultation);

					await _consultationRepository.AddAsync(consultation);
					await _consultationRepository.SaveChangesAsync();

					var consultationResponse = _mapper.Map<ConsultationResponseDTO>(consultation);

					// Buscando dentistas e paciente
					consultationResponse.Dentists = await GetDentistsByIdsAsync(dto.DentistIds);
					consultationResponse.Patient = await GetPatientByIdAsync(dto.PatientId);

					_logger.LogInformation("Created consultation with ID {ConsultationId}", consultation.Id);

					return consultationResponse;
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
		
			// HELPER METHODS

			private async Task ValidateEntitiesAsync(Guid patientId, IEnumerable<Guid> dentistIds)
			{
				if (!await ValidatePatient(patientId))
					throw new KeyNotFoundException($"Patient with id {patientId} not found.");

				foreach (var dentistId in dentistIds)
				{
					if (!await ValidateDentist(dentistId))
						throw new KeyNotFoundException($"Dentist with id {dentistId} not found.");
				}
			}

		private async Task PopulateConsultationDetails(ConsultationResponseDTO consultationResponse, IEnumerable<Consultation> consultations)
		{
			var consultation = consultations.FirstOrDefault(c => c.Id == consultationResponse.Id);

			if (consultation == null)
			{
				_logger.LogWarning("Consultation with ID {ConsultationId} not found.", consultationResponse.Id);
				return;
			}

			var dentistIds = consultation.ConsultationDentists?.Select(cd => cd.DentistId).ToList();
			if (dentistIds != null && dentistIds.Any())
			{
				consultationResponse.Dentists = await GetDentistsByIdsAsync(dentistIds);
			}
			else
			{
				_logger.LogWarning("No dentist found for consultation with ID {ConsultationId}.", consultationResponse.Id);
			}

			if (consultation.PatientId != Guid.Empty)
			{
				PatientResponseDTO a = await GetPatientByIdAsync(consultation.PatientId);
				_logger.LogInformation("{fds}", a);
				consultationResponse.Patient = a;
			}
			else
			{
				_logger.LogWarning("PatientId is empty for consultation with ID {ConsultationId}.", consultationResponse.Id);
			}
		}


		private async Task<IEnumerable<DentistResponseDTO>> GetDentistsByIdsAsync(IEnumerable<Guid> dentistIds)
			{
				IEnumerable<DentistResponseDTO> dentists = await _dentistHttpClient.GetDentistsByIdsAsync(dentistIds);
				_logger.LogInformation("{dentistas}",dentists);
					return dentists.ToList();
			}

			private async Task<PatientResponseDTO> GetPatientByIdAsync(Guid patientId)
			{
				return await _patientHttpClient.GetPatientByIdAsync(patientId);
			}

			private List<ConsultationDentist> CreateConsultationDentists(IEnumerable<Guid> dentistIds, Consultation consultation)
			{
				return dentistIds.Select(dentistId => new ConsultationDentist
				{
					DentistId = dentistId,
					Consultation = consultation
				}).ToList();
			}

			public async Task<bool> ValidatePatient(Guid patientId)
			{
				var response = await _patientHttpClient.ValidatePatientAsync($"{patientId}");
				return response.IsSuccessStatusCode;
			}

			public async Task<bool> ValidateDentist(Guid dentistId)
			{
				var response = await _dentistHttpClient.ValidateDentistAsync($"{dentistId}");
				return response.IsSuccessStatusCode;
			}
		}
	}
