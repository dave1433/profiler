using Api.Services;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[ApiController]
public class ProfilerController(IProfilerService profilerService) : ControllerBase
{
    [Route(nameof(GetAllProfiles))]
    [HttpGet]
    public async Task<ActionResult<List<Profiler>>> GetAllProfiles()
    {
        var profiles = await profilerService.GetAllProfiles();
        return profiles;
    }

    [Route(nameof(CreateProfile))]
    [HttpPost]
    public async Task<ActionResult<Profiler>> CreateProfile([FromBody] CreateProfileDto dto)
    {
       var result = await profilerService.CreateProfile(dto);
       return result;
        
    }
}