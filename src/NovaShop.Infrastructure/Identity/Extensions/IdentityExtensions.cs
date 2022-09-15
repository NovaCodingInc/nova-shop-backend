using System.Security.Principal;

namespace NovaShop.Infrastructure.Identity.Extensions;

public static class IdentityExtensions
{
    public static string GetUserId(this ClaimsPrincipal? claimsPrincipal)
    {
        var data = claimsPrincipal?.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
        return data != null ? data.Value : string.Empty;
    }

    public static string GetUserId(this IPrincipal principal)
    {
        var data = (ClaimsPrincipal)principal;
        return data.GetUserId();
    }
}