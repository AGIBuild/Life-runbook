using Volo.Abp.Modularity;

namespace Agi.LifeRunbook;

/* Inherit from this class for your domain layer tests. */
public abstract class LifeRunbookDomainTestBase<TStartupModule> : LifeRunbookTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
