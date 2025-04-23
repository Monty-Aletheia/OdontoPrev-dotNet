using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using PatientService;
using PatientService.Application.Services;
using PatientService.Infra.Data;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
	loggerConfiguration
		.ReadFrom.Configuration(context.Configuration)
		.ReadFrom.Services(services)
		.Enrich.FromLogContext();
});

//DI
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplica as migrations
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<FIAPDbContext>();
	db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health", new HealthCheckOptions
{
	ResponseWriter = async (context, report) =>
	{
		context.Response.ContentType = "application/json";
		var result = JsonSerializer.Serialize(new
		{
			status = report.Status.ToString(),
			checks = report.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() }),
			duration = report.TotalDuration.TotalSeconds
		});
		await context.Response.WriteAsync(result);
	}
});

app.UseMiddleware<RequestIdMiddleware>();

app.MapControllers();

app.Run();