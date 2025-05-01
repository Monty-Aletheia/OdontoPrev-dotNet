using Microsoft.ML;
using MlNetWorker.Models;

namespace MlNetWorker.Services
{

	public class PredictionService
	{
		private readonly PredictionEngine<InputData, PredictionResult> _predEngine;

		public PredictionService()
		{
			var mlContext = new MLContext();
			var dataView = mlContext.Data.LoadFromEnumerable(new List<InputData>());
			var pipeline = mlContext.Transforms.ApplyOnnxModel("modelo.onnx");
			var model = pipeline.Fit(dataView);

			_predEngine = mlContext.Model.CreatePredictionEngine<InputData, PredictionResult>(model);
		}

		public PredictionResult Predict(InputData input)
		{
			return _predEngine.Predict(input);
		}
	}

}
