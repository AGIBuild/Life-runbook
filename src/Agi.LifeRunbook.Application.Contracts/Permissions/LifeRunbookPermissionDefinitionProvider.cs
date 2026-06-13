using Agi.LifeRunbook.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Agi.LifeRunbook.Permissions;

public class LifeRunbookPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LifeRunbookPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(LifeRunbookPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(LifeRunbookPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(LifeRunbookPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(LifeRunbookPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LifeRunbookPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LifeRunbookResource>(name);
    }
}
