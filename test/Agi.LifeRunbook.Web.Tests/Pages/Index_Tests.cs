using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Agi.LifeRunbook.Pages;

[Collection(LifeRunbookTestConsts.CollectionDefinitionName)]
public class Index_Tests : LifeRunbookWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
