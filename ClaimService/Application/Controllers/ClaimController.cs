﻿using AutoMapper;
using ClaimService.Application.Dtos;
using ClaimService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.Application.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClaimController : ControllerBase
	{
		private readonly IClaimAppService _service;

		public ClaimController(IClaimAppService claimService)
		{
			_service = claimService;
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

		// GET: api/Claim/consultation/{id}
		[HttpGet("consultation/{id}")]
		public async Task<IActionResult> GetClaimByConsultationId(Guid id)
		{
			try
			{
				var claim = await _service.GetAllClaimsByConsultationId(id);
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