using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Aletheia.Application.Services;
using Aletheia.Application.Dtos.Consultation;

namespace Aletheia.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly ConsultationService _consultationService;
        private readonly IMapper _mapper;

        public ConsultationController(ConsultationService consultationService, IMapper mapper)
        {
            _consultationService = consultationService;
            _mapper = mapper;
        }

        // GET: api/Consultation
        [HttpGet]
        public async Task<IActionResult> GetConsultations()
        {
            var consultations = await _consultationService.GetConsultationsAsync();
            var consultationDtos = _mapper.Map<IEnumerable<ConsultationResponseDTO>>(consultations);
            return Ok(consultationDtos);
        }

        // GET: api/Consultation/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultationById(Guid id)
        {
            try
            {
                var consultation = await _consultationService.GetConsultationByIdAsync(id);
                if (consultation == null)
                    return NotFound();

                var consultationDto = _mapper.Map<ConsultationResponseDTO>(consultation);
                return Ok(consultationDto);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var consultation = await _consultationService.CreateConsultationAsync(dto);
            var createdConsultationDto = _mapper.Map<ConsultationResponseDTO>(consultation);
            return CreatedAtAction(nameof(GetConsultationById), new { id = createdConsultationDto.Id }, createdConsultationDto);
        }

        // PUT: api/Consultation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsultation(Guid id, [FromBody] UpdateConsultationDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedConsultation = await _consultationService.UpdateConsultationAsync(id, dto);
                var updatedConsultationDto = _mapper.Map<ConsultationResponseDTO>(updatedConsultation);
                return Ok(updatedConsultationDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Consultation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultation(Guid id)
        {
            try
            {
                await _consultationService.DeleteConsultationAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
