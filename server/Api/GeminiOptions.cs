using System.ComponentModel.DataAnnotations;

namespace Api;

public class GeminiOptions
{
    public bool Enabled { get; set; }

    // Allow null unless Gemini is enabled; validation is handled in AddGeminiOptions.
    public string? ApiKey { get; set; }

    [MinLength(1)]
    public string Model { get; set; } = "gemini-2.0-flash";
}

