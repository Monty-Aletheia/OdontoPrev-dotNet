using Aletheia.Presentation.Config;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injections
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddInfraRepositories();
builder.Services.AddAutoMapperProfiles();
builder.Services.AddApplicationServices();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();