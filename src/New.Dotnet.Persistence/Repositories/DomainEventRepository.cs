using Microsoft.Extensions.Logging;
using New.Dotnet.Persistence.Models;
using New.Dotnet.Persistence.Abstractions;
using New.Dotnet.Persistence.Context;

namespace New.Dotnet.Persistence.Repositories;

public class DomainEventRepository : IDomainEventRepository
{
    private readonly ILogger<DomainEventRepository> _logger;
    private readonly DatabaseContext _dbContext;
    
    public DomainEventRepository
    (
        ILogger<DomainEventRepository> logger,
        DatabaseContext dbContext
    )
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task CreateDomainEventAsync(DomainEvent domainEvent)
    {
        var result = await _dbContext.DomainEvents.AddAsync(domainEvent);
        await _dbContext.SaveChangesAsync();
    }
}