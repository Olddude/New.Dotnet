using System.Text.Json;
using Confluent.Kafka;
using New.Dotnet.Messaging.Contracts;
using New.Dotnet.Persistence.Abstractions;

namespace New.Dotnet.Services.Features.GetWeatherForecasts;

public class GetWeatherForecastsMessageHandler : IGetWeatherForecastsMessageHandler
{
    private readonly IWeatherForecastRepository _repository;

    public GetWeatherForecastsMessageHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Message<string, string>> HandleAsync(Message<string, string> requestMessage)
    {
        var requestContract = JsonSerializer.Deserialize<GetWeatherForecastsRequest>(requestMessage.Value);
        if (requestContract is null)
        {
            return new Message<string, string>
            {
                Key = requestMessage.Key,
                Value = JsonSerializer.Serialize(new ApplicationException("request contract is null"))
            };
        }
        var weatherForecasts = await _repository.GetWeatherForecastsAsync(requestContract.Page, requestContract.Take);
        var responseContract = new GetWeatherForecastsResponse
        {
            Values = weatherForecasts.Select(_ => new WeatherForecastDto
            {
                Id = _.Id,
                Temperature = _.Temperature,
                Date = _.Date,
                Message = _.Message,
                CreatedAt = _.CreatedAt
            })
        };
        var responseMessage = new Message<string, string>
        {
            Key = requestMessage.Key,
            Value = JsonSerializer.Serialize(responseContract)
        };
        return responseMessage;
    }
}