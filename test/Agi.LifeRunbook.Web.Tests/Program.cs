using Microsoft.AspNetCore.Builder;
using Agi.LifeRunbook;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Agi.LifeRunbook.Web.csproj");
await builder.RunAbpModuleAsync<LifeRunbookWebTestModule>(applicationName: "Agi.LifeRunbook.Web");

public partial class Program
{
}
