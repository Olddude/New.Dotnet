using System.Text.Json;
using AutoMapper;
using Confluent.Kafka;
using New.Dotnet.Contracts.Features.GetWeatherForecasts;
using New.Dotnet.Messaging.Kafka.Abstractions;
using New.Dotnet.Persistence.Abstractions;
using New.Dotnet.Persistence.Internal;

namespace New.Dotnet.Messaging.Kafka.Handlers;

public class GetWeatherForecastsRequestMessageHandler : IGetWeatherForecastsMessageHandler
{
    private readonly IWeatherForecastRepository _repository;
    private readonly IMapper _mapper;

    public GetWeatherForecastsRequestMessageHandler
    (
        IWeatherForecastRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Message<string, string>> HandleAsync(Message<string, string> requestMessage, CancellationToken cancellationToken)
    {
        // var data = DummyDataService.GetWeatherForecasts();
        // await _repository.CreateWeatherForecastsAsync(data, cancellationToken);
        var requestContract = JsonSerializer.Deserialize<GetWeatherForecastsRequest>(requestMessage.Value);
        if (requestContract is null)
        {
            return new Message<string, string>
            {
                Key = requestMessage.Key,
                Value = JsonSerializer.Serialize(new ApplicationException("request contract is null"))
            };
        }
        var weatherForecasts = await _repository.GetWeatherForecastsAsync(requestContract.Page, requestContract.Take, cancellationToken);
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