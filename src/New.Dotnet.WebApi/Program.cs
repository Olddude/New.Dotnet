using New.Dotnet.WebApi.Abstractions;
using New.Dotnet.WebApi.Options;
using New.Dotnet.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddTransient<IWeatherForecastsService, KafkaWeatherForecastsService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
