using Microsoft.EntityFrameworkCore;
using New.Dotnet.Messaging.Options;
using New.Dotnet.Persistence.Abstractions;
using New.Dotnet.Persistence.Context;
using New.Dotnet.Persistence.Repositories;
using New.Dotnet.Services.Features.GetWeatherForecasts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
}, ServiceLifetime.Singleton);
builder.Services.AddSingleton<IDomainEventRepository, DomainEventRepository>();
builder.Services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddSingleton<IGetWeatherForecastsMessageHandler, GetWeatherForecastsMessageHandler>();
builder.Services.AddHostedService<GetWeatherForecastsBackgroundService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
