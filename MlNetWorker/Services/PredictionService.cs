using Microsoft.ML;
using MlNetWorker.Models;
using Shared.Common.Dtos;

namespace MlNetWorker.Services
{
	public class PredictionService
	{
		private readonly PredictionEngine<InputData, PredictionResult> _predEngine;

		public PredictionService()
		{
			var mlContext = new MLContext();
			var modelPath = "AI/AletheIA.zip";

			using var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			var loadedModel = mlContext.Model.Load(stream, out var modelInputSchema);

			_predEngine = mlContext.Model.CreatePredictionEngine<InputData, PredictionResult>(loadedModel);
		}

		public PredictionResult Predict(PatientRiskAssessmentDTO input)
		{
			var inputData = PatientRiskAssessmentMapper.ToInputData(input);
			return _predEngine.Predict(inputData);
		}
	}
}