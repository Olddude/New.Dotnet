using New.Dotnet.Service.Abstractions;
using New.Dotnet.Service.Models;

namespace New.Dotnet.Service.Repositories;

public class DomainEventRepository : IDomainEventRepository
{
    private readonly ILogger<DomainEventRepository> _logger;
    private readonly ApplicationDatabaseContext _dbContext;
    
    public DomainEventRepository
    (
        ILogger<DomainEventRepository> logger,
        ApplicationDatabaseContext dbContext
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