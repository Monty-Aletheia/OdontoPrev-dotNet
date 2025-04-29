using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os do Ocelot
builder.Services.AddOcelot();

// Carrega o JSON final no Ocelot
builder.Configuration.AddJsonFile("ocelot-combined.json", optional: false, reloadOnChange: true);

var app = builder.Build();
await app.UseOcelot();
app.Run();