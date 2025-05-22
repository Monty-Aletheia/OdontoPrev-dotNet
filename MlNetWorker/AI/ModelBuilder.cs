using Microsoft.ML.Data;
using Microsoft.ML;
using MlNetWorker.Models;
using System.Security.Cryptography;

namespace MlNetWorker.AI
{
	public class ModelBuilder 
	{
		public void generateAI()
		{
			var mlContext = new MLContext();
			string dataPath = "Data/dados_sinistro.csv";
			string hashPath = "Data/dados_sinistro.hash";
			string modelPath = "AI/AletheIA.zip";

			if (!HasDataChanged(dataPath, hashPath))
			{
				Console.WriteLine("Nenhuma mudança detectada no arquivo de dados. Modelo não será gerado.");
				return;
			}

			Console.WriteLine("Mudança detectada, gerando novo modelo...");

			IDataView data = mlContext.Data.LoadFromTextFile<InputData>(
				path: dataPath,
				hasHeader: true,
				separatorChar: ',');

			var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new[]
			{
				new InputOutputColumnPair("GeneroEncoded", "Genero"),
				new InputOutputColumnPair("DoencasSistemicasEncoded", "DoencasSistemicas"),
				new InputOutputColumnPair("TipoPlanoEncoded", "TipoPlano")
			})
			.Append(mlContext.Transforms.Conversion.ConvertType(new[]
			{
				new InputOutputColumnPair("HistoricoCaries_f", nameof(InputData.HistoricoCaries)),
				new InputOutputColumnPair("DoencaPeriodontal_f", nameof(InputData.DoencaPeriodontal)),
				new InputOutputColumnPair("Fumante_f", nameof(InputData.Fumante)),
				new InputOutputColumnPair("Alcoolismo_f", nameof(InputData.Alcoolismo)),
				new InputOutputColumnPair("EscovacaoDiaria_f", nameof(InputData.EscovacaoDiaria)),
				new InputOutputColumnPair("UsoFioDental_f", nameof(InputData.UsoFioDental)),
				new InputOutputColumnPair("MedicamentosUsoContinuo_f", nameof(InputData.MedicamentosUsoContinuo)),
				new InputOutputColumnPair("TratamentosComplexosPrevios_f", nameof(InputData.TratamentosComplexosPrevios))
			}, outputKind: DataKind.Single))
			.Append(mlContext.Transforms.Concatenate("Features",
				nameof(InputData.Idade),
				"GeneroEncoded",
				nameof(InputData.FrequenciaConsultas),
				nameof(InputData.AderenciaTratamento),
				"HistoricoCaries_f",
				"DoencaPeriodontal_f",
				nameof(InputData.NumeroImplantes),
				"TratamentosComplexosPrevios_f",
				"Fumante_f",
				"Alcoolismo_f",
				"EscovacaoDiaria_f",
				"UsoFioDental_f",
				"DoencasSistemicasEncoded",
				"MedicamentosUsoContinuo_f",
				nameof(InputData.NumeroSinistrosPrevios),
				"TipoPlanoEncoded"
			))
			.Append(mlContext.Regression.Trainers.FastTree(labelColumnName: nameof(InputData.ProbabilidadeSinistro)));

			var split = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
			var trainData = split.TrainSet;

			var model = pipeline.Fit(trainData);

			mlContext.Model.Save(model, data.Schema, modelPath);
			Console.WriteLine("Modelo salvo em: " + modelPath);

			var newHash = ComputeFileHash(dataPath);
			File.WriteAllText(hashPath, newHash);
		}


		private string ComputeFileHash(string filePath)
		{
			using var sha256 = SHA256.Create();
			using var stream = File.OpenRead(filePath);
			var hashBytes = sha256.ComputeHash(stream);
			return Convert.ToBase64String(hashBytes);
		}

		private bool HasDataChanged(string dataFilePath, string hashFilePath)
		{
			var newHash = ComputeFileHash(dataFilePath);

			if (!File.Exists(hashFilePath))
			{
				return true;  
			}

			var oldHash = File.ReadAllText(hashFilePath);
			return !string.Equals(newHash, oldHash, StringComparison.Ordinal);
		}


	}
}
