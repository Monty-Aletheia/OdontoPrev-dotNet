using Aletheia.Application.Dtos.Patient;
using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using AutoMapper;

namespace Aletheia.Application.Services
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientResponseDTO>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientResponseDTO>>(patients);
        }

        public async Task<PatientResponseDTO> GetPatientsByIdAsync(Guid id)
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

    }
}
