using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientService.Application.Dtos;
using PatientService.Application.Services.Interfaces;

namespace PatientService.Application.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PatientController : ControllerBase
	{
		private readonly IPatientAppService _service;
		private readonly IMapper _mapper;

		public PatientController(IPatientAppService patientService, IMapper mapper)
		{
			_service = patientService;
			_mapper = mapper;
		}

		// GET: api/Patient
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var patients = await _service.GetAllPatientsAsync();
			var patientDtos = _mapper.Map<IEnumerable<PatientResponseDTO>>(patients);
			return Ok(patientDtos);
		}

		// GET: api/Patient/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			try
			{
				var patient = await _service.GetPatientByIdAsync(id);
				var patientDto = _mapper.Map<PatientResponseDTO>(patient);
				return Ok(patientDto);
			} catch (KeyNotFoundException ex) {
				return NotFound();
			}
		}

		// POST: api/Patient
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreatePatientDTO patientDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var patient = await _service.CreatePatientAsync(patientDto);
			var createdPatientDto = _mapper.Map<PatientResponseDTO>(patient);
			return CreatedAtAction(nameof(Get), new { id = createdPatientDto.Id }, createdPatientDto);
		}

		// PUT: api/Patient/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientDTO patientDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedPatient = await _service.UpdatePatientAsync(id, patientDto);
				if (updatedPatient == null)
					return NotFound($"Patient with id {id} not found.");

				var updatedPatientDto = _mapper.Map<PatientResponseDTO>(updatedPatient);
				return Ok(updatedPatientDto);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}

		// DELETE: api/Patient/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _service.DeletePatientAsync(id);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}