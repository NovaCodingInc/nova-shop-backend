using Microsoft.AspNetCore.Http;

namespace NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Utilities;

public interface IClaimBasedAuthorizationUtilities
{
    string? GetClaimToAuthorize(HttpContext httpContext);
}