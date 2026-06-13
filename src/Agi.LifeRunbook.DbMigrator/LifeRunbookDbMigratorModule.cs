using Agi.LifeRunbook.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Agi.LifeRunbook.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LifeRunbookEntityFrameworkCoreModule),
    typeof(LifeRunbookApplicationContractsModule)
)]
public class LifeRunbookDbMigratorModule : AbpModule
{
}
