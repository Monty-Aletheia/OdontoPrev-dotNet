using AutoMapper;
using ConsultationService.Application.Dtos;
using ConsultationService.Application.Services.HttpClients;
using ConsultationService.Application.Services.HttpClients.Interfaces;
using ConsultationService.Domain.Interfaces;
using ConsultationService.Domain.Models;
using System.Net.Http;
using System.Text.Json;

namespace ConsultationService.Application.Services
{
	public class ConsultationAppService
	{
		private readonly IConsultationRepository _consultationRepository;
		private readonly IMapper _mapper;
		private readonly IPatientHttpClient _patientHttpClient;
		private readonly IDentistHttpClient _dentistHttpClient;
		private readonly HttpClient _httpClient;

		public ConsultationAppService(IConsultationRepository consultationRepository, IMapper mapper, IPatientHttpClient patientHttpClient, IDentistHttpClient dentistHttpClient, HttpClient httpClient)
		{
			_consultationRepository = consultationRepository;
			_mapper = mapper;
			_patientHttpClient = patientHttpClient;
			_dentistHttpClient = dentistHttpClient;
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<ConsultationResponseDTO>> GetConsultationsAsync()
		{
			var consultations = await _consultationRepository.GetAllAsync();
			Console.WriteLine("Consultations: " + JsonSerializer.Serialize(consultations));
			return _mapper.Map<IEnumerable<ConsultationResponseDTO>>(consultations);
		}

		public async Task<ConsultationResponseDTO> GetConsultationByIdAsync(Guid id)
		{
			var consultation = await _consultationRepository.GetByIdAsync(id);
			if (consultation == null) throw new KeyNotFoundException($"Consultation with id {id} not found.");

			return _mapper.Map<ConsultationResponseDTO>(consultation);
		}

		public async Task<ConsultationResponseDTO> CreateConsultationAsync(CreateConsultationDTO dto)
		{
			// Validando o paciente via PatientService
			if (!await ValidatePatient(dto.PatientId))
				throw new KeyNotFoundException($"Patient with id {dto.PatientId} not found.");

			// Validando dentistas via DentistService
			foreach (var dentistId in dto.DentistIds)
			{
				if (!await ValidateDentist(dentistId))
					throw new KeyNotFoundException($"Dentist with id {dentistId} not found.");
			}

			// Mapeia DTO pra entidade Consultation
			var consultation = _mapper.Map<Consultation>(dto);

			// Adiciona a lista de dentistas na entidade de junção
			consultation.ConsultationDentists = dto.DentistIds
				.Select(dentistId => new ConsultationDentist { DentistId = dentistId, Consultation = consultation })
				.ToList();

			// Adiciona e salva tudo de uma vez (Consultation + ConsultationDentists)
			await _consultationRepository.AddAsync(consultation);
			await _consultationRepository.SaveChangesAsync();

			return _mapper.Map<ConsultationResponseDTO>(consultation);
		}


		public async Task<ConsultationResponseDTO> UpdateConsultationAsync(Guid id, UpdateConsultationDTO dto)
		{
			var consultation = await _consultationRepository.GetByIdAsync(id);
			if (consultation == null) throw new KeyNotFoundException($"Consultation with id {id} not found.");

			if (dto.PatientId.HasValue && !await ValidatePatient(dto.PatientId.Value))
				throw new KeyNotFoundException($"Patient with id {dto.PatientId.Value} not found.");

			if (dto.DentistIds != null && dto.DentistIds.Any())
			{
				foreach (var dentistId in dto.DentistIds)
				{
					if (!await ValidateDentist(dentistId))
						throw new KeyNotFoundException($"Dentist with id {dentistId} not found.");
				}

				consultation.ConsultationDentists = dto.DentistIds
					.Select(dentistId => new ConsultationDentist { DentistId = dentistId })
					.ToList();
			}

			_mapper.Map(dto, consultation);

			await _consultationRepository.UpdateAsync(consultation);

			return _mapper.Map<ConsultationResponseDTO>(consultation);
		}

		public async Task DeleteConsultationAsync(Guid id)
		{
			var consultation = await _consultationRepository.GetByIdAsync(id);
			if (consultation == null) throw new KeyNotFoundException($"Consultation with id {id} not found.");

			await _consultationRepository.DeleteAsync(consultation);
		}

		private async Task<bool> ValidatePatient(Guid patientId)
		{
			var response = await _patientHttpClient.GetAsync($"http://patient-service/api/patients/{patientId}");
			return response.IsSuccessStatusCode;
		}

		private async Task<bool> ValidateDentist(Guid dentistId)
		{
			var response = await _dentistHttpClient.GetAsync($"http://dentist-service/api/dentists/{dentistId}");
			return response.IsSuccessStatusCode;
		}
	}
}