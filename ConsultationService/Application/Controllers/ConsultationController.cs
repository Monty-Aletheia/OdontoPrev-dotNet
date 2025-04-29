using ConsultationService.Application.Dtos;
using ConsultationService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultationService.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConsultationController : ControllerBase
	{
		private readonly IConsultationAppService _service;

		public ConsultationController(IConsultationAppService consultationService)
		{
			_service = consultationService;
		}

		// GET: api/Consultation
		[HttpGet]
		public async Task<IActionResult> GetConsultations()
		{
			try
			{
				var consultations = await _service.GetConsultationsAsync();
				return Ok(consultations);
			} catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}

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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var consultation = await _service.CreateConsultationAsync(dto);
				return CreatedAtAction(nameof(GetConsultationById), new { id = consultation.Id }, consultation);
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
				return Ok(updatedConsultation);
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