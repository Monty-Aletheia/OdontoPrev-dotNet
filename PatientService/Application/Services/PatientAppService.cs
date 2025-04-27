using AutoMapper;
using PatientService.Application.Dtos;
using PatientService.Application.Services.Interfaces;
using PatientService.Domain.Interfaces;
using PatientService.Domain.Models;
using Serilog;

namespace PatientService.Application.Services
{
	public class PatientAppService : IPatientAppService
	{
		private readonly IPatientRepository _patientRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<PatientAppService> _logger;

		public PatientAppService(
			IPatientRepository patientRepository,
			IMapper mapper,
			ILogger<PatientAppService> logger)
		{
			_patientRepository = patientRepository;
			_mapper = mapper;
			_logger = logger;
		}


		public async Task<IEnumerable<PatientResponseDTO>> GetAllPatientsAsync()
		{
			var patients = await _patientRepository.GetAllAsync();
			_logger.LogInformation("Retrieved {Count} patients.", patients.Count());
			return _mapper.Map<IEnumerable<PatientResponseDTO>>(patients);
		}

		public async Task<PatientResponseDTO> GetPatientByIdAsync(Guid id)
		{
			var patient = await _patientRepository.GetByIdAsync(id);

			if (patient == null)
			{
				_logger.LogWarning("Patient with ID {PatientId} not found.", id);
				throw new KeyNotFoundException($"Patient with id {id} not found.");
			}

			_logger.LogInformation("Patient with ID {PatientId} found.", id);
			return _mapper.Map<PatientResponseDTO>(patient);
		}

		public async Task<PatientResponseDTO> CreatePatientAsync(CreatePatientDTO dto)
		{
			var patient = _mapper.Map<Patient>(dto);
			await _patientRepository.AddAsync(patient);

			_logger.LogInformation("Patient with ID {PatientId} created successfully.", patient.Id);
			return _mapper.Map<PatientResponseDTO>(patient);
		}

		public async Task<PatientResponseDTO> UpdatePatientAsync(Guid id, UpdatePatientDTO dto)
		{
			var patient = await _patientRepository.GetByIdAsync(id);
			if (patient == null)
			{
				_logger.LogWarning("Patient with ID {PatientId} not found for update.", id);
				throw new KeyNotFoundException($"Patient with id {id} not found.");
			}

			_mapper.Map(dto, patient);
			await _patientRepository.UpdateAsync(patient);

			_logger.LogInformation("Patient with ID {PatientId} updated successfully.", id);
			return _mapper.Map<PatientResponseDTO>(patient);
		}

		public async Task<bool> DeletePatientAsync(Guid id)
		{
			var patient = await _patientRepository.GetByIdAsync(id);
			if (patient == null)
			{
				_logger.LogWarning("Patient with ID {PatientId} not found for deletion.", id);
				throw new KeyNotFoundException($"Patient with id {id} not found.");
			}

			await _patientRepository.DeleteAsync(patient);

			_logger.LogInformation("Patient with ID {PatientId} deleted successfully.", id);
			return true;
		}
	}
}