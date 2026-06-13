using Volo.Abp.Settings;

namespace Agi.LifeRunbook.Settings;

public class LifeRunbookSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(LifeRunbookSettings.MySetting1));
    }
}
