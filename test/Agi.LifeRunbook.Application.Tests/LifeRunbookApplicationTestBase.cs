using Volo.Abp.Modularity;

namespace Agi.LifeRunbook;

public abstract class LifeRunbookApplicationTestBase<TStartupModule> : LifeRunbookTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
