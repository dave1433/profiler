using System.ComponentModel.DataAnnotations;

namespace Api;

public class AppOptions
{
    [MinLength(1)]
    public string DbConnectionString { get; set; }
    [MinLength(1)]
    public string JwtSecret { get; set; }
}