using New.Dotnet.Persistence.Models;

namespace New.Dotnet.Persistence.Abstractions;

public interface IDomainEventRepository
{
    Task CreateDomainEventAsync(DomainEvent domainEvent);
}