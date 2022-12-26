using New.Dotnet.Service.Models;

namespace New.Dotnet.Service.Abstractions;

public interface IDomainEventRepository
{
    Task CreateDomainEventAsync(DomainEvent domainEvent);
}