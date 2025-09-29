using System.Text.Json;
using api;
using Api;
using efscaffold.Entities;
using Infrastructure.Postgre.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine(JsonSerializer.Serialize(appOptions));

builder.Services.AddDbContext<ProfilerDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);
});

var app = builder.Build();

app.MapGet("/", (
    [FromServices]IOptionsMonitor<AppOptions>optionsMonitor,
    [FromServices] ProfilerDbContext dbContext) =>
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
