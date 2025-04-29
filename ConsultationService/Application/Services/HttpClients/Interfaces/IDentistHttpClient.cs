using ConsultationService.Application.Dtos;

namespace ConsultationService.Application.Services.HttpClients.Interfaces
{
	public interface IDentistHttpClient
	{
		public Task<HttpResponseMessage> ValidateDentistAsync(string url);

		public Task<IEnumerable<DentistResponseDTO>> GetDentistsByIdsAsync(IEnumerable<Guid> ids);
	}

}