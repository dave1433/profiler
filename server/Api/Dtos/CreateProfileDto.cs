using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;

public record CreateProfileDto(
    [Range(0,150)]int age, string firstname, string lastname, string occupation, string city, string photoUrl);