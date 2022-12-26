namespace New.Dotnet.WebApi.Options;

public record KafkaOptions
(
    string ClientId,
    string BootstrapServers,
    string GroupId,
    int SocketTimeoutMs,
    int SessionTimeoutMs,
    int AutoOffsetReset,
    string RequestsTopic,
    string ResponsesTopic
);
