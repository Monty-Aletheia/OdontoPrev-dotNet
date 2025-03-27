using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do Ocelot
builder.Services.AddOcelot();

//// Diretório das configurações de rotas
//var configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config");

//// Lê todos os arquivos JSON na pasta "Config"
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

//// Cria um JSON único combinando todas as rotas
//var finalConfig = new JObject
//{
//	["GlobalConfiguration"] = new JObject
//	{
//		["BaseUrl"] = "http://localhost:5000"
//	},
//	["Routes"] = new JArray(mergedRoutes)
//};

//// Salva a configuração combinada em um JSON temporário
//var combinedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ocelot-combined.json");
//File.WriteAllText(combinedFilePath, finalConfig.ToString());

// Carrega o JSON final no Ocelot
builder.Configuration.AddJsonFile("ocelot-combined.json", optional: false, reloadOnChange: true);

var app = builder.Build();
await app.UseOcelot();
app.Run();
