﻿using PatientService.Application.Services.Interfaces;
using PatientService.Infra.Messaging.Producers;
using Shared.Common.Dtos;

namespace PatientService.Application.Services
{

	public class PredictionMessageService : IPredictionMessageService
	{
		private readonly IMessagePublisher _publisher;

		public PredictionMessageService(IMessagePublisher publisher)
		{
			_publisher = publisher;
		}

		public async Task PublishMessageAsync(PatientRiskAssessmentDTO dto)
		{
			await _publisher.PublishAsync(dto);
		}
	}

}