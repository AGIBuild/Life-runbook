using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Agi.LifeRunbook.Localization;

namespace Agi.LifeRunbook.Web;

[Dependency(ReplaceServices = true)]
public class LifeRunbookBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<LifeRunbookResource> _localizer;

    public LifeRunbookBrandingProvider(IStringLocalizer<LifeRunbookResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
