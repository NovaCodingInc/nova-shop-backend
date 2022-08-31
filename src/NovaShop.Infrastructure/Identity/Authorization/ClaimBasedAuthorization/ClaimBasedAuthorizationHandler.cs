using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Utilities;

namespace NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization;

public class ClaimBasedAuthorizationHandler : AuthorizationHandler<ClaimBasedAuthorizationRequirement>
{
    private readonly IClaimBasedAuthorizationUtilities _utilities;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimBasedAuthorizationHandler(IClaimBasedAuthorizationUtilities utilities, IHttpContextAccessor httpContextAccessor)
    {
        _utilities = utilities;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimBasedAuthorizationRequirement requirement)
    {
        var claimToAuthorize = _utilities.GetClaimToAuthorize(_httpContextAccessor.HttpContext);

        if (string.IsNullOrWhiteSpace(claimToAuthorize))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (!context.User.Identity!.IsAuthenticated) return Task.CompletedTask;

        if (context.User.HasClaim(ClaimStore.ApplicationUserAccess, claimToAuthorize))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}