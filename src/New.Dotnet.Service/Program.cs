using Microsoft.EntityFrameworkCore;
using New.Dotnet.Database.Repositories;
using New.Dotnet.Service;
using New.Dotnet.Service.Abstractions;
using New.Dotnet.Service.Options;
using New.Dotnet.Service.Repositories;
using New.Dotnet.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
}, ServiceLifetime.Singleton);
builder.Services.AddSingleton<IDomainEventRepository, DomainEventRepository>();
builder.Services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddHostedService<KafkaMessageBrokerService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
