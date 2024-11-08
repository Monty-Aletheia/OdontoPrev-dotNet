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
            return Ok(claims);
        }

        // GET: api/Claim/{id}
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

        // PUT: api/Claim/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaim(Guid id, [FromBody] UpdateClaimDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var updatedClaim = await _claimService.UpdateClaimAsync(id, dto);
                return Ok(updatedClaim);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Claim/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(Guid id)
        {
            try
            {
                await _claimService.DeleteClaimAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
