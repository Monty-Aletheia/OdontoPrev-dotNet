using DentistService.Application.Dtos;
using DentistService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DentistService.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DentistController : ControllerBase
	{
		private readonly IDentistAppService _service;

		public DentistController(IDentistAppService dentistService)
		{
			_service = dentistService;
		}

		// GET: api/Dentist
		[HttpGet]
		public async Task<IActionResult> GetAllDentists()
		{
			var dentists = await _service.GetAllDentistsAsync();
			return Ok(dentists);
		}

		// GET: api/Dentist/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetDentistById(Guid id)
		{
			try
			{
				var dentist = await _service.GetDentistByIdAsync(id);
				if (dentist == null)
					return NotFound();

				return Ok(dentist);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}


		// POST: api/Dentist/by-ids
		[HttpPost("by-ids")]
		public async Task<ActionResult<IEnumerable<DentistResponseDTO>>> GetDentistsByIds([FromBody] DentistIdsRequestDTO requestDto)
		{
			try
			{
				var dentist = await _service.GetDentistsByIdsAsync(requestDto.DentistIds);
				if (dentist == null)
					return NotFound();

				return Ok(dentist);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred: " + ex.Message);
			}
		}

		// POST: api/Dentist
		[HttpPost]
		public async Task<IActionResult> CreateDentist([FromBody] CreateDentistDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var dentist = await _service.CreateDentistAsync(dto);
			return CreatedAtAction(nameof(GetDentistById), new { id = dentist.Id }, dentist);
		}

		// POST: api/Dentist/validate
		[HttpPost("validate")]
		public async Task<IActionResult> ValidateDentist([FromBody] LoginDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var dentist = await _service.GetDentistByRegistrationNumberAndPassword(dto);
				if (dentist == null)
					return Unauthorized(new { message = "Invalid credentials." });

				return Ok(dentist);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An error occurred while validating the dentist.", error = ex.Message });
			}
		}


		// PUT: api/Dentist/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDentist(Guid id, [FromBody] UpdateDentistDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var dentist = await _service.UpdateDentistAsync(id, dto);
				if (dentist == null)
					return NotFound($"Dentist with id {id} not found.");

				return Ok(dentist);
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
				await _service.GetDentistByIdAsync(id);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}