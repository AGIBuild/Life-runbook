using Agi.LifeRunbook.Samples;
using Xunit;

namespace Agi.LifeRunbook.EntityFrameworkCore.Applications;

[Collection(LifeRunbookTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LifeRunbookEntityFrameworkCoreTestModule>
{

}
