using Agi.LifeRunbook.Samples;
using Xunit;

namespace Agi.LifeRunbook.EntityFrameworkCore.Domains;

[Collection(LifeRunbookTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LifeRunbookEntityFrameworkCoreTestModule>
{

}
