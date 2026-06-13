using Agi.LifeRunbook.Books;
using Xunit;

namespace Agi.LifeRunbook.EntityFrameworkCore.Applications.Books;

[Collection(LifeRunbookTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<LifeRunbookEntityFrameworkCoreTestModule>
{

}
