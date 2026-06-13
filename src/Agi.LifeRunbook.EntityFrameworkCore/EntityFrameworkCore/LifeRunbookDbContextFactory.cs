using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Agi.LifeRunbook.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class LifeRunbookDbContextFactory : IDesignTimeDbContextFactory<LifeRunbookDbContext>
{
    public LifeRunbookDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        LifeRunbookEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<LifeRunbookDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));

        return new LifeRunbookDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Agi.LifeRunbook.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
