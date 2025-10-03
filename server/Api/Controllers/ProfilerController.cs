using Api.Dtos;
using Api.Services;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc; 


namespace Api.Controllers;

[ApiController]
public class ProfilerController(IProfilerService profilerService) : ControllerBase
{
    [Route(nameof(GetAllProfiles))]
    [HttpGet]
    public async Task<ActionResult<List<ProfileDto>>> GetAllProfiles()
    {
        var profiles = await profilerService.GetAllProfiles();
        return profiles;
    }

    [Route(nameof(CreateProfile))]
    [HttpPost]
    public async Task<ActionResult<ProfileDto>> CreateProfile([FromBody] CreateProfileDto dto)
    {
       var result = await profilerService.CreateProfile(dto);
       return result;
        
    }
    [Route(nameof(UpdateProfile))]
    [HttpPut]
    public async Task<ActionResult<ProfileDto>> UpdateProfile([FromBody] UpdateProfileDto dto)
    {
        var result = await profilerService.UpdateProfile(dto);
        
        return result;
    }
    [Route(nameof(DeleteProfile))]
    [HttpDelete]
    public Task<ActionResult<ProfileDto>> DeleteProfile([FromBody] DeleteProfileDto dto)
    {
        var result = profilerService.DeleteProfile(dto);
        return result;
    }
}