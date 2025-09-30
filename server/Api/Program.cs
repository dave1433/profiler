using System.Text.Json;
using api;
using Api;
using Api.Services;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine(JsonSerializer.Serialize(appOptions));
builder.Services.AddScoped<IProfilerService, ProfilerService>();
builder.Services.AddDbContext<ProfilerDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);
});

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCors();

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors(config => config
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetIsOriginAllowed(x => true));

app.MapControllers();

app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
