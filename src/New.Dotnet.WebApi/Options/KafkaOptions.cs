namespace New.Dotnet.MessageBus.Options;

public class KafkaOptions
{
    public string ClientId { get; set; }
    public string BootstrapServers { get; set; }
    public string GroupId { get; set; }
    public int SocketTimeoutMs { get; set; }
    public int SessionTimeoutMs { get; set; }
    public int AutoOffsetReset { get; set; }

    public string RequestsTopic { get; set; }

    public string ResponsesTopic { get; set; }
}