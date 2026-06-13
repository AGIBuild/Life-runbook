using Volo.Abp.Modularity;

namespace Agi.LifeRunbook;

[DependsOn(
    typeof(LifeRunbookDomainModule),
    typeof(LifeRunbookTestBaseModule)
)]
public class LifeRunbookDomainTestModule : AbpModule
{

}
