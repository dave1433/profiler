using Api.Controllers;
using efscaffold.Entities;
using Infrastructure.Postgre.Scaffolding;

namespace Api.Services;

public class ProfilerService(ProfilerDbContext dbContext) : IProfilerService
{
    public async Task<Profiler> CreateProfile(CreateProfileDto dto)
    {
        var myProfile = new Profiler()
        {
            Firstname = dto.firstname,
            Lastname = dto.lastname,
            Age = dto.age,
            City = dto.city,
            Occupation = dto.occupation,
            Photourl = dto.photoUrl
        };
        dbContext.Profilers.Add(myProfile);
        dbContext.SaveChanges();
        var objects= dbContext.Profilers.ToList();
        return myProfile;
    }

    public async Task<List<Profiler>> GetAllProfiles()
    {
        return dbContext.Profilers.ToList(); //List<Profiler>
    }
}