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

        public ClaimController(ClaimService claimService)
        {
            _claimService = claimService;
        }

        // GET: api/Claim
        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(claims);
        }

        // GET: api/Claim/2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(Guid id)
        {
            try
            {
                var claim = await _claimService.GetClaimByIdAsync(id);
                return Ok(claim);
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
                return CreatedAtAction(nameof(GetClaimById), new { id = claim.Id }, claim);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
