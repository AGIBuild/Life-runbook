using Agi.LifeRunbook.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Agi.LifeRunbook.Web.Pages;

public abstract class LifeRunbookPageModel : AbpPageModel
{
    protected LifeRunbookPageModel()
    {
        LocalizationResourceType = typeof(LifeRunbookResource);
    }
}
