using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

builder.Configuration.AddJsonFile("ocelot-combined.json", optional: false, reloadOnChange: true);

var app = builder.Build();

app.UseCors("AllowAll");

await app.UseOcelot();
app.Run();