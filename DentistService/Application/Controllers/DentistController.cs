using AutoMapper;
using DentistService.Application.Dtos;
using DentistService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DentistService.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DentistController : ControllerBase
	{
		private readonly DentistAppService _service;
		private readonly IMapper _mapper;

		public DentistController(DentistAppService dentistService, IMapper mapper)
		{
			_service = dentistService;
			_mapper = mapper;
		}

		// GET: api/Dentist
		[HttpGet]
		public async Task<IActionResult> GetAllDentists()
		{
			var dentists = await _service.GetAllDentistsAsync();
			var dentistDtos = _mapper.Map<IEnumerable<DentistResponseDTO>>(dentists);
			return Ok(dentistDtos);
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

			var dentist = await _service.CreateDentistAsync(dto);
			var createdDentistDto = _mapper.Map<DentistResponseDTO>(dentist);
			return CreatedAtAction(nameof(GetDentistById), new { id = createdDentistDto.Id }, createdDentistDto);
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

				var dentistDto = _mapper.Map<DentistResponseDTO>(dentist);
				return Ok(dentistDto);
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