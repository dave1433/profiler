using Api.Controllers;
using Api.Dtos;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IProfilerService
{
    Task<ProfileDto> CreateProfile(CreateProfileDto dto);
    Task<List<ProfileDto>> GetAllProfiles();
    Task<ActionResult<ProfileDto>> UpdateProfile(UpdateProfileDto dto);
    Task<ActionResult<ProfileDto>> DeleteProfile(DeleteProfileDto dto);
}