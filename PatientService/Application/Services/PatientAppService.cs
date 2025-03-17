using AutoMapper;
using PatientService.Application.Dtos;
using PatientService.Domain.Interfaces;
using PatientService.Domain.Models;

namespace PatientService.Application.Services
{
    public class PatientAppService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientAppService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientResponseDTO>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientResponseDTO>>(patients);
        }

        public async Task<PatientResponseDTO> GetPatientByIdAsync(Guid id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null) throw new KeyNotFoundException($"Patient with id {id} not found.");

            return _mapper.Map<PatientResponseDTO>(patient);
        }

        public async Task<PatientResponseDTO> CreatePatientAsync(CreatePatientDTO dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            await _patientRepository.AddAsync(patient);

            return _mapper.Map<PatientResponseDTO>(patient);
        }

        public async Task<PatientResponseDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) throw new KeyNotFoundException($"Patient with id {id} not found.");

            _mapper.Map(dto, patient);  
            await _patientRepository.UpdateAsync(patient);

            return _mapper.Map<PatientResponseDTO>(patient);
        }

        public async Task<bool> DeletePatientAsync(Guid id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) throw new KeyNotFoundException($"Patient with id {id} not found.");

            await _patientRepository.DeleteAsync(patient);
            return true;
        }
    }
}
