using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace New.Dotnet.Service;

public class ApplicationDatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDatabaseContext>
{
    public ApplicationDatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDatabaseContext>();
        var connectionString = "Host=localhost; Port=5432; Database=newdotnet; Username=postgres; Password=postgres;"; 
        optionsBuilder.UseNpgsql(connectionString);
        var databaseContext = new ApplicationDatabaseContext(optionsBuilder.Options);
        return databaseContext;
    }
}