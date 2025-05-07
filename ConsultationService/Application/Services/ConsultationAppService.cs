using AutoMapper;
using ConsultationService.Application.Dtos;
using ConsultationService.Application.Services.HttpClients.Interfaces;
using ConsultationService.Application.Services.Interfaces;
using ConsultationService.Domain.Interfaces;
using ConsultationService.Domain.Models;

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
		var consultationResponses = _mapper.Map<List<ConsultationResponseDTO>>(consultations);

		var tasks = consultations.Select(async consultation =>
		{
			var response = consultationResponses.First(r => r.Id == consultation.Id);
			await PopulateConsultationDetails(response, consultation);
		});

		await Task.WhenAll(tasks);
		return consultationResponses;
	}

	public async Task<ConsultationResponseDTO> GetConsultationByIdAsync(Guid id)
	{
		var consultation = await _consultationRepository.GetConsultationWithDentistsByIdAsync(id);
		if (consultation == null)
			throw new KeyNotFoundException($"Consultation with id {id} not found.");

		var response = _mapper.Map<ConsultationResponseDTO>(consultation);
		await PopulateConsultationDetails(response, consultation);
		return response;
	}

	public async Task<ConsultationResponseDTO> CreateConsultationAsync(CreateConsultationDTO dto)
	{
		try
		{
			var validateTasks = new List<Task<bool>>
			{
				ValidatePatient(dto.PatientId)
			};
			validateTasks.AddRange(dto.DentistIds.Select(ValidateDentist));

			var results = await Task.WhenAll(validateTasks);
			if (!results[0])
				throw new KeyNotFoundException($"Patient with id {dto.PatientId} not found.");

			for (int i = 1; i < results.Length; i++)
			{
				if (!results[i])
					throw new KeyNotFoundException($"Dentist with id {dto.DentistIds.ElementAt(i - 1)} not found.");
			}

			var consultation = _mapper.Map<Consultation>(dto);
			consultation.ConsultationDentists = CreateConsultationDentists(dto.DentistIds, consultation);

			await _consultationRepository.AddAsync(consultation);
			await _consultationRepository.SaveChangesAsync();

			var patientTask = GetPatientByIdAsync(dto.PatientId);
			var dentistsTask = GetDentistsByIdsAsync(dto.DentistIds);

			await Task.WhenAll(patientTask, dentistsTask);

			var response = _mapper.Map<ConsultationResponseDTO>(consultation);
			response.Patient = patientTask.Result;
			response.Dentists = dentistsTask.Result;

			_logger.LogInformation("Created consultation with ID {ConsultationId}", consultation.Id);

			return response;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error creating consultation for patient {PatientId}", dto.PatientId);
			throw;
		}
	}

	public async Task<ConsultationResponseDTO> UpdateConsultationAsync(Guid id, UpdateConsultationDTO dto)
	{
		var consultation = await _consultationRepository.GetConsultationWithDentistsByIdAsync(id);
		if (consultation == null)
			throw new KeyNotFoundException($"Consultation with id {id} not found.");

		_mapper.Map(dto, consultation);
		await _consultationRepository.UpdateAsync(consultation);

		_logger.LogInformation("Updated consultation with ID {ConsultationId}", consultation.Id);

		return _mapper.Map<ConsultationResponseDTO>(consultation);
	}

	public async Task DeleteConsultationAsync(Guid id)
	{
		var consultation = await _consultationRepository.GetByIdAsync(id);
		if (consultation == null)
			throw new KeyNotFoundException($"Consultation with id {id} not found.");

		await _consultationRepository.DeleteAsync(consultation);

		_logger.LogInformation("Deleted consultation with ID {ConsultationId}", id);
	}

	// 🔧 Helpers

	private async Task PopulateConsultationDetails(ConsultationResponseDTO response, Consultation consultation)
	{
		var dentistIds = consultation.ConsultationDentists?.Select(cd => cd.DentistId) ?? Enumerable.Empty<Guid>();

		var patientTask = consultation.PatientId != Guid.Empty
			? GetPatientByIdAsync(consultation.PatientId)
			: Task.FromResult<PatientResponseDTO>(null);

		var dentistsTask = dentistIds.Any()
			? GetDentistsByIdsAsync(dentistIds)
			: Task.FromResult<IEnumerable<DentistResponseDTO>>(Enumerable.Empty<DentistResponseDTO>());

		await Task.WhenAll(patientTask, dentistsTask);

		response.Patient = await patientTask;
		response.Dentists = await dentistsTask;
	}

	private async Task<IEnumerable<DentistResponseDTO>> GetDentistsByIdsAsync(IEnumerable<Guid> dentistIds)
	{
		var dentists = await _dentistHttpClient.GetDentistsByIdsAsync(dentistIds);
		return dentists ?? Enumerable.Empty<DentistResponseDTO>();
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
		var response = await _patientHttpClient.ValidatePatientAsync(patientId.ToString());
		return response.IsSuccessStatusCode;
	}

	public async Task<bool> ValidateDentist(Guid dentistId)
	{
		var response = await _dentistHttpClient.ValidateDentistAsync(dentistId.ToString());
		return response.IsSuccessStatusCode;
	}
}