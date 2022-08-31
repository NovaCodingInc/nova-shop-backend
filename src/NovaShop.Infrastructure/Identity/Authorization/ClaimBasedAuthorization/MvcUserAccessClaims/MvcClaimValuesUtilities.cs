namespace NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.MvcUserAccessClaims;

public static class MvcClaimValuesUtilities
{
    public static IEnumerable<(string? claimValueEnglish, string? claimValuePersian)> GetPersianAndEnglishClaimValues(Type type)
    {
        var allConstantsInTheType = type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(field => field.IsLiteral && !field.IsInitOnly)
            .ToList();

        foreach (var englishClaimValue in allConstantsInTheType.Where(m => !m.Name.Contains("Persian")))
        {
            var persianClaimValue = allConstantsInTheType
                .Single(m => m.Name == englishClaimValue.Name + "Persian");

            yield return (englishClaimValue.GetValue(null)!.ToString(), persianClaimValue.GetValue(null)!.ToString());
        }
    }
}