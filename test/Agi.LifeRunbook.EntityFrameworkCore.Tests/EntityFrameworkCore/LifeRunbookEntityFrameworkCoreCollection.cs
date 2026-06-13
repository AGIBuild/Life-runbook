using Xunit;

namespace Agi.LifeRunbook.EntityFrameworkCore;

[CollectionDefinition(LifeRunbookTestConsts.CollectionDefinitionName)]
public class LifeRunbookEntityFrameworkCoreCollection : ICollectionFixture<LifeRunbookEntityFrameworkCoreFixture>
{

}
