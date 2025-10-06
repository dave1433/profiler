using Api.Controllers;
using Api.Dtos;
using efscaffold.Entities;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ProfilerService(ProfilerDbContext dbContext) : IProfilerService
{
    public async Task<ProfileDto> CreateProfile(CreateProfileDto dto)
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
        await dbContext.SaveChangesAsync();
        return ProfileDto.FromEntity(myProfile);
    }

    public async Task<List<ProfileDto>> GetAllProfiles()
    {
        var profiles = await dbContext.Profilers.ToListAsync();
        return profiles.Select(ProfileDto.FromEntity).ToList(); //List<Profiles>
    }

    public async Task<ActionResult<ProfileDto>> UpdateProfile(UpdateProfileDto dto)
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
       return new OkObjectResult(ProfileDto.FromEntity(profile));
    }

    public async Task<ActionResult<ProfileDto>> DeleteProfile(DeleteProfileDto dto)
    {
        var profile = await dbContext.Profilers.FindAsync(dto.Id);
        if (profile == null)
            return new NotFoundResult();

        dbContext.Profilers.Remove(profile);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(ProfileDto.FromEntity(profile));
    }
}