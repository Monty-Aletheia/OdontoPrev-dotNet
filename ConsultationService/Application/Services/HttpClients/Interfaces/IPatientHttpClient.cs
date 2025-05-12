using ConsultationService.Application.Dtos;

namespace ConsultationService.Application.Services.HttpClients.Interfaces
{
	public interface IPatientHttpClient
	{
		public Task<HttpResponseMessage> ValidatePatientAsync(string url);

		public Task<PatientResponseDTO> GetPatientByIdAsync(Guid id);

	}
}