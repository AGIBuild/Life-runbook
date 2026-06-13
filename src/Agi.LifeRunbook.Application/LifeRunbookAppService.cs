using Agi.LifeRunbook.Localization;
using Volo.Abp.Application.Services;

namespace Agi.LifeRunbook;

/* Inherit your application services from this class.
 */
public abstract class LifeRunbookAppService : ApplicationService
{
    protected LifeRunbookAppService()
    {
        LocalizationResource = typeof(LifeRunbookResource);
    }
}
