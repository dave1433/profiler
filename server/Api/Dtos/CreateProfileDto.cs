using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public record CreateProfileDto(
    [Range(0,150)]int Age, string Firstname, string Lastname, string Occupation, string City, string PhotoUrl);