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

			if (!File.Exists(modelPath))
			{
				throw new FileNotFoundException($"Modelo de IA não encontrado em: {modelPath}. Execute o treinamento primeiro.");
			}

			try
			{
				using var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
				var loadedModel = mlContext.Model.Load(stream, out var modelInputSchema);

				_predEngine = mlContext.Model.CreatePredictionEngine<InputData, PredictionResult>(loadedModel);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Erro ao carregar o modelo de IA.", ex);
			}
		}

		public PredictionResult Predict(PatientRiskAssessmentDTO input)
		{
			var inputData = PatientRiskAssessmentMapper.ToInputData(input);
			return _predEngine.Predict(inputData);
		}
	}
}
