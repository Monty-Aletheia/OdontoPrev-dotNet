using AutoMapper;
using Aletheia.Application.Dtos.Claim;
using Aletheia.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aletheia.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly ClaimService _claimService;
        private readonly IMapper _mapper;

        public ClaimController(ClaimService claimService, IMapper mapper)
        {
            _claimService = claimService;
            _mapper = mapper;
        }

        // GET: api/Claim
        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            var claimDtos = _mapper.Map<IEnumerable<ClaimResponseDTO>>(claims);
            return Ok(claimDtos);
        }

        // GET: api/Claim/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(Guid id)
        {
            try
            {
                var claim = await _claimService.GetClaimByIdAsync(id);
                var claimDto = _mapper.Map<ClaimResponseDTO>(claim);
                return Ok(claimDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Claim
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromBody] CreateClaimDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var claim = await _claimService.CreateClaimAsync(dto);
                var claimDto = _mapper.Map<ClaimResponseDTO>(claim);
                return CreatedAtAction(nameof(GetClaimById), new { id = claimDto.Id }, claimDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
