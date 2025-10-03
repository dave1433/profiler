using Xunit;
using Api.Services;
using Api.Dtos;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class ProfilerServiceTests
{
    [Fact]
    public async Task CreateProfile_ReturnsProfileDto()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ProfilerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        using var dbContext = new ProfilerDbContext(options);
        var service = new ProfilerService(dbContext);
        var dto = new CreateProfileDto(30, "John", "Doe", "NY", "Engineer", "photo.jpg");

        // Act
        var result = await service.CreateProfile(dto);

        // Assert
        Assert.Equal("John", result.Firstname);
        Assert.Equal("Doe", result.Lastname);
    }
}