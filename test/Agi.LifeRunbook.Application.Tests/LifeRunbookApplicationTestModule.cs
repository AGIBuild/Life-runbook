using Volo.Abp.Modularity;

namespace Agi.LifeRunbook;

[DependsOn(
    typeof(LifeRunbookApplicationModule),
    typeof(LifeRunbookDomainTestModule)
)]
public class LifeRunbookApplicationTestModule : AbpModule
{

}
