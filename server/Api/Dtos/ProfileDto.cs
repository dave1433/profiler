using efscaffold.Entities;

namespace Api.Dtos;

public class ProfileDto(
    int id,
    int age,
    string firstname,
    string lastname,
    string occupation,
    string city,
    string photoUrl)
{
    public int Id { get; set; } = id;
    public string Firstname { get; set; } = firstname;
    public string Lastname { get; set; } = lastname;
    public int Age { get; set; } = age;
    public string City { get; set; } = city;
    public string Occupation { get; set; } = occupation;
    public string PhotoUrl { get; set; } = photoUrl;

    public static ProfileDto FromEntity(Profiler profiler) =>
        new ProfileDto(
            profiler.Id,
            profiler.Age,
            profiler.Firstname,
            profiler.Lastname,
            profiler.Occupation,
            profiler.City,
            profiler.Photourl
        );
}