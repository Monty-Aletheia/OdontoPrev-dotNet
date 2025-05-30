﻿using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace DentistService.Application.Dtos
{
	public class CreateDentistDTO
	{
		[Required]
		public required string Name { get; set; }

		public string Specialty { get; set; }

		[Required]
		public required string RegistrationNumber { get; set; }

		public double? ClaimsRate { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		[EnumDataType(typeof(RiskStatus))]
		public string RiskStatus { get; set; }
	}
}