using Api.Controllers;
using Xunit;
using Api.Services;
using Api.Dtos;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class ProfilerServiceTests
{
    [Fact]
    public async Task CreateProfile_ReturnsProfileDto()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ProfilerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_Create")
            .Options;
        using var dbContext = new ProfilerDbContext(options);
        var service = new ProfilerService(dbContext);
        var dto = new CreateProfileDto(30, "John", "Doe", "NY", "Engineer", "https://shorturl.at/virJa");

        // Act
        var result = await service.CreateProfile(dto);

        // Assert
        Assert.Equal("John", result.Firstname);
        Assert.Equal("Doe", result.Lastname);
    }

    [Fact]
    public async Task UpdateProfile_ChangesProfileData()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ProfilerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_Update")
            .Options;
        using var dbContext = new ProfilerDbContext(options);
        var service = new ProfilerService(dbContext);
        var createDto = new CreateProfileDto(30, "John", "Doe", "NY", "Engineer", "");
        var created = await service.CreateProfile(createDto);
        var updateDto = new UpdateProfileDto(created.Id, 31, "Jane", "Doe", "Manager", "SF", "");

        // Act
        var updated = await service.UpdateProfile(updateDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(updated.Result);
        var profile = Assert.IsType<ProfileDto>(okResult.Value);
        Assert.Equal("SF", profile.City);
        Assert.Equal("Manager", profile.Occupation);
        Assert.Equal(31, profile.Age);
    }

    [Fact]
    public async Task DeleteProfile_RemovesProfile()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ProfilerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_Delete")
            .Options;
        using var dbContext = new ProfilerDbContext(options);
        var service = new ProfilerService(dbContext);
        var dto = new CreateProfileDto(25, "Alice", "Smith", "LA", "QA", "");
        var created = await service.CreateProfile(dto);

        // Act
        var deleteDto = new DeleteProfileDto { Id = created.Id };
        var result = await service.DeleteProfile(deleteDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Null(await dbContext.Profilers.FindAsync(created.Id));
    }
}