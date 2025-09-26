using efscaffold.Entities;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProfilerDbContext>(conf =>
{
    conf.UseNpgsql();
});

var app = builder.Build();

app.MapGet("/", ([FromServices] ProfilerDbContext dbContext) =>
{
    var myProfile = new Profiler()
    {
        Firstname = "John",
        Lastname = "Doe",
        Age = 30,
        City = "New York",
        Occupation = "Software Developer",
        Photourl = "https://example.com/photo.jpg"
    };
    dbContext.Profilers.Add(myProfile);
    dbContext.SaveChanges();
   var objects= dbContext.Profilers.ToList();
   return objects;
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
