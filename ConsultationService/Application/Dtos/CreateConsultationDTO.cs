﻿using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConsultationService.Application.Dtos
{
	public class CreateConsultationDTO
	{
		[Required]
		public DateTime ConsultationDate { get; set; }

		[Required]
		public double? ConsultationValue { get; set; }

		[Required]
		[EnumDataType(typeof(RiskStatus))]
		public string RiskStatus { get; set; }

		[Required]
		public Guid PatientId { get; set; }

		[Required]
		public required ICollection<Guid> DentistIds { get; set; }

		public string? Description { get; set; }

	}
}