using AutoMapper;
using ConsultationService.Application.Dtos;
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
        private readonly HttpClient _httpClient;

        public ConsultationAppService(
            IConsultationRepository consultationRepository,
            IMapper mapper,
            HttpClient httpClient)
        {
            _consultationRepository = consultationRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ConsultationResponseDTO>> GetConsultationsAsync()
        {
            var consultations = await _consultationRepository.GetAllAsync();
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

            var consultation = _mapper.Map<Consultation>(dto);
            consultation.ConsultationDentists = dto.DentistIds
                .Select(dentistId => new ConsultationDentist { DentistId = dentistId })
                .ToList();

            await _consultationRepository.AddAsync(consultation);

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
            var response = await _httpClient.GetAsync($"http://patient-service/api/patients/{patientId}");
            return response.IsSuccessStatusCode;
        }

        private async Task<bool> ValidateDentist(Guid dentistId)
        {
            var response = await _httpClient.GetAsync($"http://dentist-service/api/dentists/{dentistId}");
            return response.IsSuccessStatusCode;
        }
    }
}
