using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using New.Dotnet.Persistence.Context;

namespace New.Dotnet.Persistence;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        var connectionString = "Host=localhost; Port=5432; Database=newdotnet; Username=postgres; Password=postgres;"; 
        optionsBuilder.UseNpgsql(connectionString);
        var databaseContext = new DatabaseContext(optionsBuilder.Options);
        return databaseContext;
    }
}
