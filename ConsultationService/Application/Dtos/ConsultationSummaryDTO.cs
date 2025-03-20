namespace ConsultationService.Application.Dtos
{
	public class ConsultationSummaryDTO
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public double? ConsultationValue { get; set; }
	}
}