using System.Text;
using Confluent.Kafka;

namespace New.Dotnet.Messaging.Kafka.Serializers;

public class MessageKeySerializer : ISerializer<Guid>
{ 
    public byte[] Serialize(Guid data, SerializationContext context)
    {
        return Encoding.UTF8.GetBytes(data.ToString());
    }
}