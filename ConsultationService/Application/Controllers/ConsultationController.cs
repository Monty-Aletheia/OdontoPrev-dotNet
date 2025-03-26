using AutoMapper;
using ConsultationService.Application.Dtos;
using ConsultationService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsultationService.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConsultationController : ControllerBase
	{
		private readonly ConsultationAppService _service;
		private readonly IMapper _mapper;

		public ConsultationController(ConsultationAppService consultationService, IMapper mapper)
		{
			_service = consultationService;
			_mapper = mapper;
		}

		// GET: api/Consultation
		[HttpGet]
		public async Task<IActionResult> GetConsultations()
		{
			var consultations = await _service.GetConsultationsAsync();
			var consultationDtos = _mapper.Map<IEnumerable<ConsultationResponseDTO>>(consultations);
			return Ok(consultationDtos);
		}

		// GET: api/Consultation/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetConsultationById(Guid id)
		{
			try
			{
				var consultation = await _service.GetConsultationByIdAsync(id);
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

			try
			{
				var consultation = await _service.CreateConsultationAsync(dto);
				var createdConsultationDto = _mapper.Map<ConsultationResponseDTO>(consultation);
				return CreatedAtAction(nameof(GetConsultationById), new { id = createdConsultationDto.Id }, createdConsultationDto);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}

		// PUT: api/Consultation/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateConsultation(Guid id, [FromBody] UpdateConsultationDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedConsultation = await _service.UpdateConsultationAsync(id, dto);
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
				await _service.DeleteConsultationAsync(id);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}