namespace NovaShop.Infrastructure.Identity.Authorization.ClaimBasedAuthorization.Utilities.MvcNamesUtilities;

public class MvcNamesModel : IEquatable<MvcNamesModel>
{
    public MvcNamesModel(string? areaName, string? controllerName, string? actionName, string? claimToAuthorize)
    {
        AreaName = areaName;
        ControllerName = controllerName;
        ActionName = actionName;
        ClaimToAuthorize = claimToAuthorize;
        IsClaimBasedAuthorizationRequired = !string.IsNullOrWhiteSpace(claimToAuthorize);
    }

    public MvcNamesModel(string? areaName, string? controllerName, string? actionName)
    {
        AreaName = areaName;
        ControllerName = controllerName;
        ActionName = actionName;
        ClaimToAuthorize = null;
        IsClaimBasedAuthorizationRequired = false;
    }

    public string? AreaName { get; }
    public string? ControllerName { get; }
    public string? ActionName { get; }
    public string? ClaimToAuthorize { get; }
    public bool IsClaimBasedAuthorizationRequired { get; }

    public bool Equals(MvcNamesModel? other)
    {
        // If parameter is null, return false.
        if (ReferenceEquals(other, null)) return false;

        // Optimization for a common success case.
        if (ReferenceEquals(this, other)) return true;

        // If run-time types are not exactly the same, return false.
        if (GetType() != other.GetType()) return false;

        return AreaName == other.AreaName
               && ControllerName == other.ControllerName
               && ActionName == other.ActionName;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as MvcNamesModel);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AreaName, ControllerName, ActionName);
    }
}