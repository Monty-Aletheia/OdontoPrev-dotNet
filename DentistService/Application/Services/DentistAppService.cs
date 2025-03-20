using AutoMapper;
using DentistService.Application.Dtos;
using DentistService.Domain.Interfaces;
using DentistService.Domain.Models;

namespace DentistService.Application.Services
{
	public class DentistAppService
	{
		private readonly IDentistRepository _dentistRepository;
		private readonly IMapper _mapper;

		public DentistAppService(IDentistRepository dentistRepository, IMapper mapper)
		{
			_dentistRepository = dentistRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<DentistResponseDTO>> GetAllDentistsAsync()
		{
			var dentists = await _dentistRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<DentistResponseDTO>>(dentists);
		}

		public async Task<DentistResponseDTO> GetDentistByIdAsync(Guid id)
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

		public async Task<DentistResponseDTO> UpdateDentistAsync(Guid id, UpdateDentistDTO dto)
		{
			var dentist = await _dentistRepository.GetByIdAsync(id);
			if (dentist == null) throw new KeyNotFoundException($"Dentist with id {id} not found.");

			_mapper.Map(dto, dentist);
			await _dentistRepository.UpdateAsync(dentist);

			return _mapper.Map<DentistResponseDTO>(dentist);
		}

		public async Task DeleteDentistAsync(Guid id)
		{
			var dentist = await _dentistRepository.GetByIdAsync(id);
			if (dentist == null) throw new KeyNotFoundException($"Dentist with id {id} not found.");

			await _dentistRepository.DeleteAsync(dentist);
		}
	}
}