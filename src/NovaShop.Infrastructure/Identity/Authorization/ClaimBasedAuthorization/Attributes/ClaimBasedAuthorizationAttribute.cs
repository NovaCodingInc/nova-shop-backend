using Microsoft.AspNetCore.Authorization;

namespace NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ClaimBasedAuthorizationAttribute : AuthorizeAttribute
{
    public ClaimBasedAuthorizationAttribute(string claimToAuthorize) : base("ClaimBasedAuthorization")
    {
        ClaimToAuthorize = claimToAuthorize;
    }

    public string ClaimToAuthorize { get; }
}