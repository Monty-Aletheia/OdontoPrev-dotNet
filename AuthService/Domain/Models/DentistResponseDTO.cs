﻿using Shared.Enums;

namespace AuthService.Domain.Models
{
	public class DentistResponseDTO
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Specialty { get; set; }

		public string RegistrationNumber { get; set; }

		public double? ClaimsRate { get; set; }

		public string RiskStatus { get; set; }

	}
}