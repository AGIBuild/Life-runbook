using Agi.LifeRunbook.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Agi.LifeRunbook.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LifeRunbookController : AbpControllerBase
{
    protected LifeRunbookController()
    {
        LocalizationResource = typeof(LifeRunbookResource);
    }
}
