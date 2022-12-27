using Microsoft.EntityFrameworkCore;
using New.Dotnet.Messaging.Kafka.Options;
using New.Dotnet.Messaging.RabbitMq.Options;
using New.Dotnet.Persistence.Abstractions;
using New.Dotnet.Persistence.Context;
using New.Dotnet.Persistence.Repositories;
using New.Dotnet.Services.Abstractions;
using New.Dotnet.Services.Features.GetWeatherForecasts;
using New.Dotnet.Services.Handlers;
using New.Dotnet.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(typeof(WeatherForecastMapper));
});
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
}, ServiceLifetime.Singleton);
builder.Services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddSingleton<IGetWeatherForecastsMessageHandler, GetWeatherForecastsRequestMessageHandler>();
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
