﻿
namespace DentistService.Application.Dtos
{
	public class UpdateDentistDTO
	{
		public string? Name { get; set; }

		public string? Specialty { get; set; }

		public string? RegistrationNumber { get; set; }

		public double? ClaimsRate { get; set; }

		public string? RiskStatus { get; set; }
	}
}