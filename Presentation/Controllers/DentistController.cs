using Aletheia.Application.Dtos.Dentist;
using Aletheia.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aletheia.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentistController : ControllerBase
    {
        private readonly DentistService _dentistService;

        public DentistController(DentistService dentistService)
        {
            _dentistService = dentistService;
        }

        // GET: api/Dentist
        [HttpGet]
        public async Task<IActionResult> GetAllDentists()
        {
            var dentists = await _dentistService.GetAllDentintsAsync();
            return Ok(dentists);
        }

        // GET: api/Dentist/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDentistById(Guid id)
        {
            try
            {
                var dentist = await _dentistService.GetPatientByIdAsync(id);
                return Ok(dentist);
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
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var dentist = await _dentistService.CreateDentistAsync(dto);
            return CreatedAtAction(nameof(GetDentistById), new { id = dentist.Id }, dentist);
        }
    }
}
