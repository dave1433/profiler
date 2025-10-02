using Api.Controllers;
using Api.Dtos;
using efscaffold.Entities;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class ProfilerService(ProfilerDbContext dbContext) : IProfilerService
{
    public async Task<Profiler> CreateProfile(CreateProfileDto dto)
    {
        var myProfile = new Profiler()
        {
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            Age = dto.Age,
            City = dto.City,
            Occupation = dto.Occupation,
            Photourl = dto.PhotoUrl
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

    public async Task<ActionResult<Profiler>> UpdateProfile(UpdateProfileDto dto)
    {
       var profile = await dbContext.Profilers.FindAsync(dto.Id);
       if(profile == null)
           return new NotFoundResult();
       
       if (dto.Firstname != null)profile.Firstname = dto.Firstname;
       if (dto.Lastname != null)profile.Lastname = dto.Lastname;
       if (dto.Age.HasValue)profile.Age = dto.Age.Value;
       if (dto.City != null)profile.City = dto.City;
       if (dto.Occupation != null)profile.Occupation = dto.Occupation;
       if (dto.PhotoUrl != null)profile.Photourl = dto.PhotoUrl;
       await dbContext.SaveChangesAsync();
       return new OkObjectResult(profile);
    }

    public async Task<ActionResult<Profiler>> DeleteProfile(DeleteProfileDto dto)
    {
        var profile = await dbContext.Profilers.FindAsync(dto.Id);
        if (profile == null)
            return new NotFoundResult();

        dbContext.Profilers.Remove(profile);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(profile);
    }
}