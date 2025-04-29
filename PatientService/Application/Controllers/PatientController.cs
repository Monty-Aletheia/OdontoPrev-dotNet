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

		public PatientController(IPatientAppService patientService)
		{
			_service = patientService;
		}

		// GET: api/Patient
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var patients = await _service.GetAllPatientsAsync();
			return Ok(patients);
		}

		// GET: api/Patient/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			try
			{
				var patient = await _service.GetPatientByIdAsync(id);
				return Ok(patient);
			}
			catch (KeyNotFoundException ex)
			{
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
			return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
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

				return Ok(updatedPatient);
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