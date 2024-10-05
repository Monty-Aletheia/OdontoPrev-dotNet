using Aletheia.Application.Dtos.Dentist;
using Aletheia.Domain.Entities;
using Aletheia.Domain.Interfaces;
using AutoMapper;

namespace Aletheia.Application.Services
{
    public class DentistService
    {
        private readonly IDentistRepository _dentistRepository;
        private readonly IMapper _mapper;

        public DentistService(IDentistRepository dentistRepository, IMapper mapper)
        {
            _dentistRepository = dentistRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DentistResponseDTO>> GetAllDentintsAsync()
        {
            var dentints = await _dentistRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DentistResponseDTO>>(dentints);
        }

        public async Task<DentistResponseDTO> GetPatientByIdAsync(Guid id)
        {
            var dentist = await _dentistRepository.GetByIdAsync(id);

            if (dentist == null) throw new KeyNotFoundException($"Dentist with id {id} not found.");

            return _mapper.Map<DentistResponseDTO>(dentist);
        }

        public async Task<DentistResponseDTO> CreateDentistAsync(CreateDentistDTO dto)
        {
            var dentist = _mapper.Map<Dentist>(dto);
            await _dentistRepository.AddAsync(dentist);

            return _mapper.Map<DentistResponseDTO>(dentist);
        }
    }
}
