using System.ComponentModel.DataAnnotations;

namespace Api;

public static class GeminiOptionsExtensions
{
    public static GeminiOptions AddGeminiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var geminiOptions = new GeminiOptions();
        configuration.GetSection(nameof(GeminiOptions)).Bind(geminiOptions);

        var apiKeyFromEnv = configuration["GEMINI_API_KEY"];
        if (!string.IsNullOrWhiteSpace(apiKeyFromEnv))
        {
            geminiOptions.ApiKey = apiKeyFromEnv;
        }

        services.Configure<GeminiOptions>(options =>
        {
            configuration.GetSection(nameof(GeminiOptions)).Bind(options);

            if (!string.IsNullOrWhiteSpace(apiKeyFromEnv))
            {
                options.ApiKey = apiKeyFromEnv;
            }
        });

        ICollection<ValidationResult> results = new List<ValidationResult>();
        var validated = Validator.TryValidateObject(geminiOptions, new ValidationContext(geminiOptions), results, true);
        if (!validated)
        {
            throw new Exception($"Gemini options validation failed: {string.Join(", ", results.Select(r => r.ErrorMessage))}");
        }

        if (geminiOptions.Enabled && string.IsNullOrWhiteSpace(geminiOptions.ApiKey))
        {
            throw new Exception(
                "Gemini is enabled but no API key was provided. Configure GeminiOptions:ApiKey or GEMINI_API_KEY.");
        }

        return geminiOptions;
    }
}

