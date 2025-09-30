using Api.Controllers;
using efscaffold.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public interface IProfilerService
{
    Task<Profiler> CreateProfile(CreateProfileDto dto);
    Task<List<Profiler>> GetAllProfiles();
}