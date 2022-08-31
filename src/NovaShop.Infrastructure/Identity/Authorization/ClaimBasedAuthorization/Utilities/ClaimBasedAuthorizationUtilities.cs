using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities;

namespace NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Utilities;

public class ClaimBasedAuthorizationUtilities : IClaimBasedAuthorizationUtilities
{
    private readonly IMvcUtilities _mvcUtilities;

    public ClaimBasedAuthorizationUtilities(IMvcUtilities mvcUtilities)
    {
        _mvcUtilities = mvcUtilities;
    }

    public string? GetClaimToAuthorize(HttpContext httpContext)
    {
        var areaName = httpContext.GetRouteValue("area")?.ToString();
        var controllerName = httpContext.GetRouteValue("controller")?.ToString();
        var actionName = httpContext.GetRouteValue("action")?.ToString();

        _mvcUtilities.MvcInfoForActionsThatRequireClaimBasedAuthorization
            .TryGetValue(new MvcNamesModel(areaName, controllerName, actionName),
                out var actualValue);

        return actualValue?.ClaimToAuthorize;
    }
}