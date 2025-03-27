using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os do Ocelot
builder.Services.AddOcelot();

//// Diret�rio das configura��es de rotas
//var configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config");

//// L� todos os arquivos JSON na pasta "Config"
//var jsonFiles = Directory.GetFiles(configPath, "*.json");

//var mergedRoutes = new List<JToken>();

//foreach (var file in jsonFiles)
//{
//	var jsonContent = File.ReadAllText(file);
//	var jsonObject = JObject.Parse(jsonContent);

//	if (jsonObject["Routes"] != null)
//	{
//		mergedRoutes.AddRange(jsonObject["Routes"]);
//	}
//}

//// Cria um JSON �nico combinando todas as rotas
//var finalConfig = new JObject
//{
//	["GlobalConfiguration"] = new JObject
//	{
//		["BaseUrl"] = "http://localhost:5000"
//	},
//	["Routes"] = new JArray(mergedRoutes)
//};

//// Salva a configura��o combinada em um JSON tempor�rio
//var combinedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ocelot-combined.json");
//File.WriteAllText(combinedFilePath, finalConfig.ToString());

// Carrega o JSON final no Ocelot
builder.Configuration.AddJsonFile("ocelot-combined.json", optional: false, reloadOnChange: true);

var app = builder.Build();
await app.UseOcelot();
app.Run();
