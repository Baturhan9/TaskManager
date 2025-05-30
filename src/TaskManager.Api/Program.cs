using System.Text.Json.Serialization;
using TaskManager.Api.Classes.Utilities;
using TaskManager.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModelsServices();

builder.Services.AddMyAuthentication(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();

// Configurations
builder.Services.ConfigureCors();
builder.Services.ConfigureDataBase(builder.Configuration);
builder.Services.ConfigureAutoMapper();

builder.Services.AddSingleton<FilesHandler>();

var app = builder.Build();

app.UseExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
