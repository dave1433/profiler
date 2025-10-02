using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;

public record UpdateProfileDto(
    int Id,
    [Range(0,150)]
    int? Age,
    string? Firstname,
    string? Lastname,
    string? Occupation,
    string? City,
    string? PhotoUrl);