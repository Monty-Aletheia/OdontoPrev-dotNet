using Aletheia.Application.Dtos.Consultation;
using Aletheia.Domain.Entities;
using Aletheia.Domain.Entities.Enum;
using Aletheia.Domain.Interfaces;
using AutoMapper;

namespace Aletheia.Application.Services
{
    public class ConsultationService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDentistRepository _dentistRepository;
        private readonly IConsultationRepository _consultationRepository;
        private readonly IMapper _mapper;

        public ConsultationService(IPatientRepository petientRepository, IDentistRepository dentistRepository, IConsultationRepository consultationRepository, IMapper mapper)
        {
            _patientRepository = petientRepository;
            _dentistRepository = dentistRepository;
            _consultationRepository = consultationRepository;
            _mapper = mapper;
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
            var patient = await _patientRepository.GetByIdAsync(dto.PatientId);
            if (patient == null) throw new KeyNotFoundException($"Patient with id {dto.PatientId} not found.");

            var dentists = new List<Dentist>();
            foreach (var dentistId in dto.DentistIds)
            {
                var dentist = await _dentistRepository.GetByIdAsync(dentistId)
                ?? throw new KeyNotFoundException($"Dentist with id {dentistId} not found.");
                dentists.Add(dentist);
            }

            var consultation = _mapper.Map<Consultation>(dto);
            consultation.Patient = patient;
            consultation.Dentists = dentists;

            await _consultationRepository.AddAsync(consultation);

            return _mapper.Map<ConsultationResponseDTO>(consultation);
        }

    }
}
