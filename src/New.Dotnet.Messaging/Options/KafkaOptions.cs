namespace New.Dotnet.Messaging.Options;

public record KafkaOptions
{
    public string? ClientId { get; init; }
    public string? BootstrapServers { get; init; }
    public string? GroupId { get; init; }
    public int SocketTimeoutMs { get; init; }
    public int SessionTimeoutMs { get; init; }
    public int AutoOffsetReset { get; init; }
    public string? RequestsTopic { get; init; }
    public string? ResponsesTopic { get; init; }
}
