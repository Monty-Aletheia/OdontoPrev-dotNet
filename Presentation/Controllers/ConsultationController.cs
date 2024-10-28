using Aletheia.Application.Dtos.Consultation;
using Aletheia.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aletheia.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly ConsultationService _consultationService;

        public ConsultationController(ConsultationService consultationService)
        {
            _consultationService = consultationService;
        }

        // GET: api/Consultation
        [HttpGet]
        public async Task<IActionResult> GetConsultations()
        {
            var consultations = await _consultationService.GetConsultationsAsync();
            return Ok(consultations);
        }

        // GET: api/Consultation/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultationById(Guid id)
        {
            try
            {
                var consultation = await _consultationService.GetConsultationByIdAsync(id);
                return Ok(consultation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Consultation
        [HttpPost]
        public async Task<IActionResult> CreateConsultation([FromBody] CreateConsultationDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var consultation = await _consultationService.CreateConsultationAsync(dto);
                return CreatedAtAction(nameof(GetConsultationById), new { id = consultation.Id }, consultation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    
    }
}
