using Api.Controllers;
using Api.Dtos;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IProfilerService
{
    Task<Profiler> CreateProfile(CreateProfileDto dto);
    Task<List<Profiler>> GetAllProfiles();
    Task<ActionResult<Profiler>> UpdateProfile(UpdateProfileDto dto);
    Task<ActionResult<Profiler>> DeleteProfile(DeleteProfileDto dto);
}