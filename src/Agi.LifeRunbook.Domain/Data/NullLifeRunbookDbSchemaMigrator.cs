using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Agi.LifeRunbook.Data;

/* This is used if database provider does't define
 * ILifeRunbookDbSchemaMigrator implementation.
 */
public class NullLifeRunbookDbSchemaMigrator : ILifeRunbookDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
