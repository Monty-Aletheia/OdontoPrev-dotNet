namespace Aletheia.Application.Dtos.Consultation
{
    public class ConsultationSummaryDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double? ConsultationValue { get; set; }
    }
}
