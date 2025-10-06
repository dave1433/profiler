using Infrastructure.Postgre.Scaffolding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace Tests;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        Program.ConfigureServices(services, configuration);
        services.RemoveAll(typeof(ProfilerDbContext));

        services.AddScoped<ProfilerDbContext>(factory =>
        {
            var postgreSqlContainer = new PostgreSqlBuilder().Build();
            postgreSqlContainer.StartAsync().GetAwaiter().GetResult();
            var connectionString = postgreSqlContainer.GetConnectionString();
            var options = new DbContextOptionsBuilder<ProfilerDbContext>()
                .UseNpgsql(connectionString)
                .Options;
            
            var ctx = new ProfilerDbContext(options);
            ctx.Database.EnsureCreated();
            
            //seed
            
            return ctx;
        });
    }
}