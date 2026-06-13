using System.Threading.Tasks;

namespace Agi.LifeRunbook.Data;

public interface ILifeRunbookDbSchemaMigrator
{
    Task MigrateAsync();
}
