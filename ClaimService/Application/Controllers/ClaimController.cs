using AutoMapper;
using ClaimService.Application.Dtos;
using ClaimService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.Application.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClaimController : ControllerBase
	{
		private readonly ClaimAppService _service;
		private readonly IMapper _mapper;

		public ClaimController(ClaimAppService claimService, IMapper mapper)
		{
			_service = claimService;
			_mapper = mapper;
		}

		// GET: api/Claim
		[HttpGet]
		public async Task<IActionResult> GetAllClaims()
		{
			var claims = await _service.GetAllClaimsAsync();
			return Ok(claims);
		}

		// GET: api/Claim/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetClaimById(Guid id)
		{
			try
			{
				var claim = await _service.GetClaimByIdAsync(id);
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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var claim = await _service.CreateClaimAsync(dto);
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
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedClaim = await _service.UpdateClaimAsync(id, dto);
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
				await _service.DeleteClaimAsync(id);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}