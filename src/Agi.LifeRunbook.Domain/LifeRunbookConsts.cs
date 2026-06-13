using Volo.Abp.Identity;

namespace Agi.LifeRunbook;

public static class LifeRunbookConsts
{
    public const string DbTablePrefix = "App";
    public const string? DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordConfigurationKey = "Identity:AdminPassword";
}
