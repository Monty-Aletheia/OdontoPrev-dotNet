using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Aletheia.Application.Services;
using Aletheia.Application.Dtos.Patient;

namespace Aletheia.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(PatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            var patientDtos = _mapper.Map<IEnumerable<PatientResponseDTO>>(patients);
            return Ok(patientDtos);
        }

        // GET: api/Patient/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var patient = await _patientService.GetPatientsByIdAsync(id);
            if (patient == null)
                return NotFound();

            var patientDto = _mapper.Map<PatientResponseDTO>(patient);
            return Ok(patientDto);
        }

        // POST: api/Patient
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDTO patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await _patientService.CreatePatientAsync(patientDto);
            var createdPatientDto = _mapper.Map<PatientResponseDTO>(patient);
            return CreatedAtAction(nameof(Get), new { id = createdPatientDto.Id }, createdPatientDto);
        }
    }
}
