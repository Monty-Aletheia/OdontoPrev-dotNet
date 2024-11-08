using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Aletheia.Application.Services;
using Aletheia.Application.Dtos.Dentist;

namespace Aletheia.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistController : ControllerBase
    {
        private readonly DentistService _dentistService;
        private readonly IMapper _mapper;

        public DentistController(DentistService dentistService, IMapper mapper)
        {
            _dentistService = dentistService;
            _mapper = mapper;
        }

        // GET: api/Dentist
        [HttpGet]
        public async Task<IActionResult> GetAllDentists()
        {
            var dentists = await _dentistService.GetAllDentistsAsync();
            var dentistDtos = _mapper.Map<IEnumerable<DentistResponseDTO>>(dentists);
            return Ok(dentistDtos);
        }

        // GET: api/Dentist/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDentistById(Guid id)
        {
            try
            {
                var dentist = await _dentistService.GetDentistByIdAsync(id);
                if (dentist == null)
                    return NotFound();

                var dentistDto = _mapper.Map<DentistResponseDTO>(dentist);
                return Ok(dentistDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Dentist
        [HttpPost]
        public async Task<IActionResult> CreateDentist([FromBody] CreateDentistDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dentist = await _dentistService.CreateDentistAsync(dto);
            var createdDentistDto = _mapper.Map<DentistResponseDTO>(dentist);
            return CreatedAtAction(nameof(GetDentistById), new { id = createdDentistDto.Id }, createdDentistDto);
        }

        // PUT: api/Dentist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDentist(Guid id, [FromBody] UpdateDentistDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dentist = await _dentistService.UpdateDentistAsync(id, dto);
                if (dentist == null)
                    return NotFound($"Dentist with id {id} not found.");

                var updatedDentistDto = _mapper.Map<DentistResponseDTO>(dentist);
                return Ok(updatedDentistDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Dentist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDentist(Guid id)
        {
            try
            {
                await _dentistService.GetDentistByIdAsync(id);
                return NoContent(); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
