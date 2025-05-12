using Shared.Common.Dtos;

namespace PatientService.Application.Services.Interfaces
{
	public interface IPredictionMessageService
	{
		Task PublishMessageAsync(PatientRiskAssessmentDTO dto);
	}
}