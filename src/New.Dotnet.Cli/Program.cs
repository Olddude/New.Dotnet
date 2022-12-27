using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using New.Dotnet.Cli.Services;
using New.Dotnet.Persistence.Abstractions;
using New.Dotnet.Persistence.Context;
using New.Dotnet.Persistence.Repositories;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("appsettings.Development.json", false, true)
    .Build();

Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder =>
    {
        builder.AddConsole();
    })
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DatabaseContext>(builder =>
        {
            builder.UseNpgsql(context.Configuration.GetConnectionString("Database"));
        }, ServiceLifetime.Singleton);

        services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
        
        services.AddHostedService<CreateDummyDataService>();
    })
    .Build()
    .RunAsync();