using System.Text.Json;
using Api;
using Api.Services;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class Program
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppOptions>(configuration.GetSection(nameof(AppOptions)));
        services.AddScoped<IProfilerService, ProfilerService>();
        services.AddDbContext<ProfilerDbContext>((provider, conf) =>
        {
            var appOptions = provider.GetRequiredService<IOptions<AppOptions>>().Value;
            conf.UseNpgsql(appOptions.DbConnectionString);
        });
        
        services.AddControllers();
        services.AddOpenApiDocument(configure => configure.Title = "Profiler API");
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddCors();

    }


    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        ConfigureServices(builder.Services, builder.Configuration);

        var appOptions = builder.Services.BuildServiceProvider()
            .GetRequiredService<IOptions<AppOptions>>().Value;
        Console.WriteLine(JsonSerializer.Serialize(appOptions));

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
        await app.GenerateApiClientsFromOpenApi("/../../client/src/generated-ts-client.ts");

        app.Run();
    }
}

