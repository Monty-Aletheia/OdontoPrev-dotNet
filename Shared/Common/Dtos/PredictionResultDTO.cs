namespace Shared.Common.Dtos
{
	public class PredictionResultDTO
	{
		public Guid PatientID { get; set; }
		public float RiskScore { get; set; }
	}

}