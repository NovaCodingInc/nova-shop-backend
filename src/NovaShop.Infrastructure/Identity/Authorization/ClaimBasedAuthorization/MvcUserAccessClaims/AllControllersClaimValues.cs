using System.Collections.ObjectModel;

namespace NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims;

public static class AllControllersClaimValues
{
    public static readonly ReadOnlyCollection<(string claimValueEnglish, string claimValuePersian)> AllClaimValues;

    static AllControllersClaimValues()
    {
        var allClaimValues = new List<(string claimValueEnglish, string claimValuePersian)>();

        //TODO: Add Controller path to auth

        AllClaimValues = allClaimValues.AsReadOnly();
    }
}