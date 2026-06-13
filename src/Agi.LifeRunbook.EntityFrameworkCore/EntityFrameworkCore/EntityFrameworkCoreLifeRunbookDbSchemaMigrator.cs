using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Agi.LifeRunbook.Data;
using Volo.Abp.DependencyInjection;

namespace Agi.LifeRunbook.EntityFrameworkCore;

public class EntityFrameworkCoreLifeRunbookDbSchemaMigrator
    : ILifeRunbookDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLifeRunbookDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the LifeRunbookDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LifeRunbookDbContext>()
            .Database
            .MigrateAsync();
    }
}
